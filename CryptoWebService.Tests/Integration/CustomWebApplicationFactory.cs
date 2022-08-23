using CryptoWebService.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CryptoWebService.Tests.Integration;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup: class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

            if (descriptor != null) 
                services.Remove(descriptor);

            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<DataContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            try
            {
                InitialiseDatabaseSampleData(db);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }

            db.Database.EnsureCreated();
        });
    }

    private static void InitialiseDatabaseSampleData(DataContext dataContext)
    {
        
    }
}