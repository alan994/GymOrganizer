using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Helper.Configuration;
using Data.Db;

namespace RabbitMQ
{
    public class Worker
    {        
        private readonly AppSettings _settings;
        private readonly ILogger<Worker> _logger;        

        public Worker(AppSettings appSettings, ILoggerFactory loggerFactory)
        {
            this._settings = appSettings;
            this._logger = loggerFactory.CreateLogger<Worker>();
        }

        public void Start(string[] args)
        {
            this._logger.LogInformation($"RabbitMQ is started with parameter: {(args.Length == 0 ? "Empty" : args[0].ToString())} ");

            var factory = new ConnectionFactory() { HostName = this._settings.RabbitMQ.Host };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var exchange = this._settings.RabbitMQ.Exchange;

                channel.ExchangeDeclare(exchange: exchange, type: "topic");

                var queueName = "";
                                
                #region Args configuration
                if (args.Length == 0)
                {
                    queueName = channel
                   .QueueDeclare(this._settings.RabbitMQ.QueueName, true, false, false, null)
                   .QueueName;

                    channel.QueueBind(queue: queueName, exchange: this._settings.RabbitMQ.Exchange, routingKey: "#");
                }

                else if (args.Length == 1)
                {
                    queueName = channel
                   .QueueDeclare(args[0], true, false, false, null)
                   .QueueName;

                    channel.QueueBind(queue: queueName, exchange: this._settings.RabbitMQ.Exchange, routingKey: $"{args[0]}.#");
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
                    using (GymOrganizerContext db = new GymOrganizerContext(this._settings.Data.Model.ConnectionString))
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

            
        }


        
    }
}
