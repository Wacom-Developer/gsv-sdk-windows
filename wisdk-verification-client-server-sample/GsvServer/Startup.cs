using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using GsvPersistence;

namespace GsvService
{
    /// <summary>
    /// The API Startup class.
    /// </summary>
    class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Configures the API services before running.
        /// </summary>
        /// <param name="services">The IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);
            services.AddControllersWithViews();
            string dataSource = _configuration.GetValue<string>("SqliteDataSource");
            if (dataSource != null)
            { 
                services.RegisterSqlitePersistence(dataSource);
            }
            else
            {
                throw new Exception("Configuration does not contain key: SqliteDataSource");
            }
            //services.RegisterIdentity();           

            services.AddTransient<GsvSignatureEngineService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "GSV API",
                        Version = "v1"
                    }
                 );

                var filePath = Path.Combine(AppContext.BaseDirectory, "GsvServer.xml");
                c.IncludeXmlComments(filePath);
            });
        }
               

        /// <summary>
        /// Configure the API.
        /// </summary>
        /// <param name="app">The IApplicationBuilder</param>
        /// <param name="env">The IWebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseAuthorityMiddleware();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GSV API");
            });

            app.UseMvc();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
