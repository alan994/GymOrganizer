using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Configuration
{
    public class AppSettings
    {
        public Data Data { get; set; }
        public RabbitMQ RabbitMQ { get; set; }
    }

    public class Auth
    {
        public string OpertationConnectionString { get; set; }
        public string ConfigurationConnectionString { get; set; }
    }

    public class Model
    {
        public string ConnectionString { get; set; }
    }

    public class Logs
    {
        public string connectionString { get; set; }
        public int MinimumLogLevel { get; set; }
    }

    public class Data
    {
        public Auth Auth { get; set; }
        public Model Model { get; set; }
        public Logs Logs { get; set; }
    }

    public class RabbitMQ
    {
        public string Host { get; set; }
        public string QueueName { get; set; }
        public string Exchange { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
