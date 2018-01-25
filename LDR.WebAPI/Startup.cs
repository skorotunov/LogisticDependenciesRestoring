using LDR.WebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace LDR.WebAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LDRContext>(x => x.UseInMemoryDatabase("LogisticDependenciesRestoring"));
            services.AddMvc();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(
                    "v1",
                    new Info
                    {
                        Title = "LDR API",
                        Version = "v1",
                        Description = "Initial LDR ASP.NET Core Web API",
                        TermsOfService = "None",
                        Contact = new Contact { Name = "Sergey Korotunov", Email = "skorotunov@yahoo.com", Url = "https://www.linkedin.com/in/skorotunov" },
                        License = new License { Name = "Use under some licemse", Url = "https://example.com/license" }
                    });

                string basePath = AppContext.BaseDirectory;
                string xmlPath = Path.Combine(basePath, "LDR.WebAPI.xml");
                x.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "LDR API V1");
            });
            app.UseMvc();
        }
    }
}
