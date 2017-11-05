using Helper.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets<Program>()
                    .AddEnvironmentVariables();
            IConfigurationRoot configuration = builder.Build();


            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton(configuration)
            .BuildServiceProvider();

            AppSettings appSettings = configuration.Get<AppSettings>();

            var connectionStringLog = appSettings.Data.Model.ConnectionString;


            serviceProvider.GetService<ILoggerFactory>().AddConsole();

            ILogger<Program> logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            logger.LogInformation($"Application is started with args: {JsonConvert.SerializeObject(args)}");

                        
            int minimumLogLevel = appSettings.Data.Logs.MinimumLogLevel;
                        
            Worker worker = new Worker(appSettings, serviceProvider.GetService<ILoggerFactory>());
            worker.Start(args);

        }
    }
}
