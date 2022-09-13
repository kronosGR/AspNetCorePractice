using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyTested.AspNetCore.Mvc;

namespace EmployeeManagement;

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
        services.ReplaceRazorRuntimeCompilation();
        services.AddMvc(options => options.EnableEndpointRouting = false);
        services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
        if (env.IsDevelopment())
        {
            var developerExceptionPageExtensions = new DeveloperExceptionPageOptions
            {
                // show 10 lines
                SourceCodeLineCount = 10
            };
            app.UseDeveloperExceptionPage(developerExceptionPageExtensions);
        }

        app.UseRouting();

        // DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
        // defaultFilesOptions.DefaultFileNames.Clear();
        // defaultFilesOptions.DefaultFileNames.Add("foo.html");
        // app.UseDefaultFiles(); // must be above UseStaticFiles and loads default/index.html
        // app.UseDefaultFiles(defaultFilesOptions);
        app.UseStaticFiles();

        // use the homeController
        //app.UseMvcWithDefaultRoute();

        //used for normal routing
        app.UseMvc(routes =>
        {
            // is like the above UseMvcWithDefaultRoute
            // with default values and optional id
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });

        //FileServerOptions fileServerOptions = new FileServerOptions();
        //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
        //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");

        //app.UseFileServer();  // instead of usestaticfile and usedefaultfiles

        // use for attribute routing
        //app.UseMvc();

        app.Use(async (context, next) =>
        {
            logger.LogInformation("Mw1: Incoming");
            await next();
            logger.LogInformation("Mw1: outgoing");
        });
    }
}