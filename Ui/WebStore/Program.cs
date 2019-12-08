using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebStore.DAL;
using WebStore.DomainNew;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope()) // нужно для получения DbContext
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<WebStoreContext>();

                try
                {
                    DbInitializer.Initialize(context);
                    DbInitializer.InitializeUsers(services);

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger>();
                    logger.LogError(ex, "OOps. Something went wrong...");
                }
            }

            host.Run();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}