using Microsoft.AspNetCore.Hosting;
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
                    var runtimeConfiguration = string.Join(CommonConstants.OptionSeparator, args);
                    webBuilder
                        .UseStartup<Startup>()
                        .UseSetting(CommonConstants.RuntimeConfiguration, args.Length > 0 ? runtimeConfiguration : null);
                }));
    }
}
