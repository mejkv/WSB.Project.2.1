using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace AirShop.AirService
{
    public class AirService : ServiceBase
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<AirService> logger;

        public AirService()
        {
            ServiceName = "AirShopAirService";
            AutoLog = true;

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();

            logger = serviceProvider.GetRequiredService<ILogger<AirService>>();
        }

        protected override void OnStart(string[] args)
        {
            logger.LogInformation($"{DateTime.Now}: Service started.");

            // Tutaj możesz uruchomić logowanie operacji bazodanowych
        }

        protected override void OnStop()
        {
            logger.LogInformation($"{DateTime.Now}: Service stopped.");

            // Tutaj możesz dodać kod zatrzymujący logowanie operacji bazodanowych
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            // Dodaj inne serwisy, które mogą być potrzebne w Twojej usłudze
        }
    }
}
