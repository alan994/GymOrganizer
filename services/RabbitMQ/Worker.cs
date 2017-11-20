using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Helper.Configuration;
using Data.Db;
using Data.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Handlers;
using BusinessLogic.Model;

namespace RabbitMQ
{
    public class Worker
    {
        private readonly AppSettings settings;
        private readonly ILoggerFactory loggerFactory;
        private readonly ILogger<Worker> logger;

        public Worker(AppSettings appSettings, ILoggerFactory loggerFactory)
        {
            this.settings = appSettings;
            this.loggerFactory = loggerFactory;
            this.logger = loggerFactory.CreateLogger<Worker>();
        }

        public void Start(string[] args)
        {
            this.logger.LogInformation($"RabbitMQ is started with parameter: {(args.Length == 0 ? "Empty" : args[0].ToString())} ");

            var factory = new ConnectionFactory() { HostName = this.settings.RabbitMQ.Host, UserName = "guest", Password = "guest" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var exchange = this.settings.RabbitMQ.Exchange;

                channel.ExchangeDeclare(exchange: exchange, type: "topic");

                var queueName = "";

                #region Args configuration
                if (args.Length == 0)
                {
                    queueName = channel
                   .QueueDeclare(this.settings.RabbitMQ.QueueName, true, false, false, null)
                   .QueueName;

                    channel.QueueBind(queue: queueName, exchange: this.settings.RabbitMQ.Exchange, routingKey: "#");
                }

                else if (args.Length == 1)
                {
                    queueName = channel
                   .QueueDeclare(args[0], true, false, false, null)
                   .QueueName;

                    channel.QueueBind(queue: queueName, exchange: this.settings.RabbitMQ.Exchange, routingKey: $"{args[0]}.#");
                }
                #endregion



                Console.WriteLine("Waiting for messages...");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;

                    Console.WriteLine($"Debug: Received '{routingKey}':'{message}'");
                    using (GymOrganizerContext db = new GymOrganizerContext(this.settings.Data.Model.ConnectionString))
                    {
                        DoWork(message, channel, ea.DeliveryTag, db).GetAwaiter().GetResult();
                    }
                    Console.WriteLine("-------------------------------------------------------------");
                };

                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

                Console.ReadLine();
            }
        }

        private void Nack(IModel channel, ulong deliveryTag)
        {
            channel.BasicNack(deliveryTag, false, false);
        }

        private void Ack(IModel channel, ulong deliveryTag)
        {
            channel.BasicAck(deliveryTag, false);
        }

        private async Task DoWork(string message, IModel channel, ulong deliveryTag, GymOrganizerContext db)
        {
            if (string.IsNullOrEmpty(message))
            {
                Nack(channel, deliveryTag);
                return;
            }


            ProcessQueueHistory request = null;
            try
            {

                var queueRequestId = Guid.Parse(message);
                request = await db.ProcessQueuesHistory.Where(x => x.Id == queueRequestId).FirstOrDefaultAsync();


                using (GymOrganizerContext processContext = new GymOrganizerContext(this.settings.Data.Model.ConnectionString))
                {

                    IHandler handler = HandlerFactory.GetHandler(request.Type, this.loggerFactory, processContext, this.settings);

                    if (handler == null)
                    {

                        this.logger.LogError($"Method DoWork: Error processing request: {message}. Request type is not supported.");
                        Nack(channel, deliveryTag);
                    }

                    QueueResult response = await handler.Handle(request.Data);
                    response.TenantId = request.TenantId;
                    response.UserId = request.AddedById;
                    response.RequestOueueId = request.Id;


                    await NotifyApi(request, response);
                    Ack(channel, deliveryTag);

                    request.FinishedAt = DateTime.UtcNow;
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Method DoWork: Error processing request: {message}. MEESAGE: {ex.Message}");
                if (request != null)
                {
                    request.Status = Data.Enums.ResultStatus.Error;
                    request.FinishedAt = DateTime.UtcNow;
                    request.ErrorMesage = ex.Message;
                    await db.SaveChangesAsync();
                }
                Nack(channel, deliveryTag);
            }

        }

        private async Task<bool> NotifyApi(ProcessQueueHistory request, QueueResult response)
        {
            //Call web api for notification update
            for (int i = 1; i <= 3; i++)
            {
                using (var client = new HttpClient())
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(response), Encoding.UTF8, "application/json");
                    try
                    {

                        HttpResponseMessage httpResponse = await client.PostAsync($"{this.settings.WebApplicationUrl}/api/notifications", stringContent);
                        if (httpResponse.StatusCode == HttpStatusCode.OK)
                        {
                            return true;
                        }
                        else if (i == 3)
                        {
                            this.logger.LogError($"Method NotifyApi: Failed to notify api for request {JsonConvert.SerializeObject(response)}");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogError($"Method NotifyApi: Error while calling signalR api {JsonConvert.SerializeObject(response)}. MESSAGE: {ex.Message}");
                    }
                }
            }
            return false;
        }



    }
}
