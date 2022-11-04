using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PictogramasApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBodySize = null;
                    });
                });
                //}).ConfigureLogging((hostingContext, loggingBuilder) =>
                //{
                //    loggingBuilder.AddFile("Logs/myapp-{Date}.txt");
                //});
    }
}
