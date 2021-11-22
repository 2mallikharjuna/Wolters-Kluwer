using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

using WkApi.Repositories.MapperProfiles;

namespace WkApi.App_Start
{
    public static class AutoMapperConfiguration
    {

        /// <summary>
        /// create extension which can be reused by integration test setup
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(                
                 typeof(WkApiMapperProfile).Assembly               
            );

            return services;
        }
    }
}
