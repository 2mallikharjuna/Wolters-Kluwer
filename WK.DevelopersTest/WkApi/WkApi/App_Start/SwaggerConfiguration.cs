using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WkApi.App_Start
{
    /// <summary>
    /// Swagger bootstrap configuration
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Enable swagger for the project
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder EnableSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger()
            .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WkApi v1"));

            return app;
        }

        /// <summary>
        /// Configure the swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "WkApi Data Hub Api",
                    Description = "WkApi Data Hub Api"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(filePath);
            });
            return services;
        }
    }
}
