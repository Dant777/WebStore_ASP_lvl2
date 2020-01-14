using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebStore
{
    public class Program
    {
        private static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(
                repo,
                log4NetConfig["log4net"]);

            Log.Info("Application launched...");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}