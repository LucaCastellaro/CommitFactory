using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommitFactory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults((System.Action<IWebHostBuilder>)(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseSetting("IsLogEnabled", args.Length > 0 ? args[0] : null);
                }));
    }
}
