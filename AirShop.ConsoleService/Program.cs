using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AirShop.AirService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation($"{DateTime.Now}: API started.");

            try
            {
                var healthCheckUrl = "http://localhost:5000/api/health"; // Dostosuj adres URL do twojego API

                using (var client = new HttpClient(new HttpLoggingHandler(new HttpClientHandler(), logger)))
                {
                    var response = await client.GetAsync(healthCheckUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        logger.LogInformation("Database connection is successful.");
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        logger.LogError($"Error connecting to the database: {content}");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error connecting to the database.");
            }

            // Logger middleware dla operacji na konsoli
            var originalConsoleOut = Console.Out;
            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            try
            {
                // Logowanie operacji bazodanowych i innych
                logger.LogInformation($"\n{DateTime.Now}: Console operation started.");

                // Tutaj możesz umieścić kod, który chciałbyś monitorować

                logger.LogInformation("Console operation completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during console operation.");
            }
            finally
            {
                // Przywróć oryginalne wyjście konsoli
                Console.SetOut(originalConsoleOut);
            }

            Console.WriteLine("Press any key to close this window . . .");
            Console.ReadKey();
            logger.LogInformation("Application is closing...");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.AddConsole());
            services.AddTransient<HttpLoggingHandler>();
            services.AddHttpClient("WebApiPostgreClient")
                .AddHttpMessageHandler<HttpLoggingHandler>();

            // Dodaj inne serwisy, które mogą być potrzebne w Twojej aplikacji
        }
    }
}
