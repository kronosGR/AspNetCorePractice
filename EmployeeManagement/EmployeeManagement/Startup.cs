using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        // is needed for accessing configuration files - appsettings.json
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                logger.LogInformation("Mw1: Incoming");
                await next();
                logger.LogInformation("Mw1: outgoing");
            });


            app.Use(async (context, next) =>
            {
                logger.LogInformation("Mw2: Incoming");
                await next();
                logger.LogInformation("Mw2: outgoing");
            });


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("2nd middleware");
                logger.LogInformation("Mw3: outgoing");
            });
        }
    }
}
