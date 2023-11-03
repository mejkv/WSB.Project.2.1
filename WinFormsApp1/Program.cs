using System;
using System.Threading;
using System.Windows.Forms;
using MagazynEdu.ApplicationsServices.API.Domain;
using MagazynEdu.ApplicationsServices.Mappings;
using MagazynEdu.DataAccess.CQRS;
using MagazynEdu.DataAccess;
using MagazynEdu.Windows.Forms.MagazynEdu.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MagazynEdu.Windows.Forms
{
    internal static class Program
    {
        private static SplashScreenForm splashScreen;

        [STAThread]
        static async Task Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var splashScreen = new SplashScreenForm();
            // Inicjalizacja Splash Screen
            //Thread splashThread = new Thread(new ThreadStart(ShowSplashScreen));
            //splashThread.Start();

            // Konfiguracja usług
            var serviceProvider = ConfigureServices();

            // Wykonaj operacje ładowania (symulowanej)
            await splashScreen.SimulateLoadingOperationAsync();
            // Możesz tutaj umieścić kod, który musi być wykonany przed otwarciem głównego formularza
            // ...

            // Zamknij Splash Screen
            splashScreen.Invoke(new Action(splashScreen.CloseSplashScreen));

            // Utwórz główny formularz i uruchom aplikację
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            Application.Run(mainForm);
        }

        private static async Task ShowSplashScreen()
        {
            splashScreen = new SplashScreenForm();
            Application.Run(splashScreen);
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Konfiguracja usług dla aplikacji desktopowej
            services.AddTransient<IQueryExecutor, QueryExecutor>();
            services.AddTransient<DataAccess.CQRS.ICommandExecutor, CommandExecutor>();

            services.AddAutoMapper(typeof(DevicesProfile));

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(typeof(ResponseBase<>).Assembly);
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddDbContext<WarehouseStorageContext>(opt =>
                opt.UseSqlServer("YourConnectionString")); // Dodaj swoje dane połączeniowe

            // Dodaj Mainform jako Scoped service
            services.AddScoped<MainForm>(); // Zamiast MainForm podaj nazwę swojego głównego formularza

            return services.BuildServiceProvider();
        }
    }
}
