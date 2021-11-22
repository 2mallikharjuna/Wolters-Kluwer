using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WkApi.Repositories.Data;
using Microsoft.Extensions.DependencyInjection;

namespace WkApiApi.Tests
{
    public class APIWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public IConfiguration Configuration { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<WkApiDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<WkApiDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<WkApiDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<APIWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();
                }
            });
        }
    }   
}
