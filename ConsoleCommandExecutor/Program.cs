using System;
using System.Collections.Generic;
using ConsoleCommandExecutor;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommandExecutorConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting CCE");
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    //config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<CceUpdate>();
                })
                .Build();

            var app = new CommandLineApplication();
            app.HelpOption("-h|--help");

            var commands = new Dictionary<string, Func<CommandBase>>
            {
                { "-u|--update", () => builder.Services.GetRequiredService<CceUpdate>() }
                // Dodaj inne komendy tutaj
            };

            foreach (var commandPair in commands)
            {
                var option = app.Option(commandPair.Key, "Apply database migrations", CommandOptionType.NoValue);
                var commandFactory = commandPair.Value;

                app.OnExecute(() =>
                {
                    if (option.HasValue())
                    {
                        var command = commandFactory();
                        command.Execute();
                    }
                    else
                    {
                        app.ShowHelp();
                    }
                });
            }

            app.Execute(args);
        }
    }
}
