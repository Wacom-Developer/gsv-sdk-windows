using System;
using System.Windows.Forms;

using Microsoft.Extensions.DependencyInjection;

using GsvClient;
using GsvClient.Models;

namespace WacomInkVerificationSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                Form mainForm = null;
                try
                {
                    mainForm = serviceProvider.GetRequiredService<Main>();
                }
                catch
                {
                }
                if (mainForm != null)
                {
                    Application.Run(mainForm);
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var clientConfig = new GsvClientConfiguration();
            if (clientConfig.ServerAddress == null)
                clientConfig.ServerAddress = "http://localhost:5001/";
            //if (clientConfig.ApiKey == null)
            //    clientConfig.ApiKey = "";
            clientConfig.ClientName = "WacomInkVerificationSample";

            services.AddSingleton(clientConfig);
            services.AddTransient<IGsvClient, WacomInkVerificationSample.GsvClient>((s) => new WacomInkVerificationSample.GsvClient(s.GetService<GsvClientConfiguration>()));
            services.AddSingleton<Main>();
        }
    }
}
