using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GsvService
{
    /// <summary>
    /// API Entry Point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main API Method.
        /// </summary>
        /// <param name="args">Main Arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Creates an IHostBuilder for the API services.
        /// </summary>
        /// <param name="args">IHostBuilder creation arguments.</param>
        /// <returns>The IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
