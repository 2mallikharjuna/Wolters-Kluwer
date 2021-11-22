using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WkApi.Repositories;
using WkApi.Repositories.Base;
using WkApi.Services;

namespace WkApi.App_Start
{
    /// <summary>
    /// Dependency injector of all services
    /// </summary>
    public static class DependencyInjector
    {
        /// <summary>
        /// Extension method to add the dependencies
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration">Base configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<IConfiguration>(configuration);
            services.AddSingleton(configuration);
            services.InjectDependencies(configuration);
            services.AddHealthChecks();
            services.AddCors(options => options.AddPolicy("ApiCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            return services;
        }

        /// <summary>
        /// Create a dependency injection
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration"></param>
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            //sql connection
            services.ConfigureAutoMapper();            
        }
    }
}
