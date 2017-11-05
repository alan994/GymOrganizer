using Data.Db;
using Data.Model;
using Helper.Configuration;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Queue
{
    public class RabbitMQHandler : IQueueHandler
    {
        private AppSettings _settings;
        private GymOrganizerContext _db;

        public RabbitMQHandler(AppSettings settings, GymOrganizerContext db)
        {
            this._settings = settings;
            this._db = db;
        }

        public async Task AddToQueue(ProcessQueueHistory request)
        {
            this._db.ProcessQueuesHistory.Add(request);
            await this._db.SaveChangesAsync();

            var routingKey = GetRoutingKeyFromProcessType();
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                //UserName = _configuration["RabbitMQ:username"],
                //Password = _configuration["RabbitMQ:password"]
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: this._settings.RabbitMQ.Exchange, type: "topic");

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = Encoding.UTF8.GetBytes(request.Data);
                channel.BasicPublish(exchange: this._settings.RabbitMQ.Exchange, routingKey: routingKey, basicProperties: properties, body: body);
            }
        }

               private string GetRoutingKeyFromProcessType()
        {
            return "general";
        }
    }
}