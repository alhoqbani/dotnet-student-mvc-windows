using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StudentMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentMVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Mvc Service. Required to use MVC
            services.AddMvc();

            // Register db with Entity
            services.AddDbContext<StudentsDataContext>(options =>
            {
                //// USE SQL Server
                //// Get connection string from config.json to use sql server.
                //var connectionString = "";
                // options.UseSqlServer(connectionString);

                // Use sqlitesqlite
                options.UseSqlite($"Data Source=Students.sqlite");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // This will serve static files from wwwroot
            app.UseFileServer();

            // Activate MVC and configre app routes.
            app.UseMvc(routes =>
            {
                routes.MapRoute("Default",
                                "{controller=Home}/{action=Index}/{id:int?}");
            });

        }
    }
}
