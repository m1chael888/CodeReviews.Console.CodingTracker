using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CodingTracker.m1chael888.Controllers;
using CodingTracker.m1chael888.Views;
using CodingTracker.m1chael888.Infrastructure;
using CodingTracker.m1chael888.Services;
using CodingTracker.m1chael888.Repositories;
using System.Text;

namespace CodingTracker.m1chael888
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();
            string connString = config.GetConnectionString("source");

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<ISessionController, SessionController>();
            serviceCollection.AddScoped<IDbInitializer>(x => new DbInitializer(connString));
            serviceCollection.AddScoped<IMainMenuView, MainMenuView>();
            serviceCollection.AddScoped<ISessionView, SessionView>();
            serviceCollection.AddScoped<ISessionService, SessionService>();
            serviceCollection.AddScoped<ISessionRepo>(x => new SessionRepo(connString));

            var serviceProdiver = serviceCollection.BuildServiceProvider();

            var initializer = serviceProdiver.GetRequiredService<IDbInitializer>();
            initializer.Initialize();

            var controller = serviceProdiver.GetRequiredService<ISessionController>();
            controller.CallMainMenu();
        }
    }
}
