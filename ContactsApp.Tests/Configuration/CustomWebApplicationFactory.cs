using ContactsApp.API;
using ContactsApp.Domain.Interfaces;
using ContactsApp.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactsApp.Tests.Configuration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var basePath = AppContext.BaseDirectory;
                var jsonFilePath = Path.Combine(basePath, "appsettings_test.json");
                config.AddJsonFile(jsonFilePath, optional: false, reloadOnChange: true);
            });

            builder.ConfigureServices(services =>
            {
                var dbContextOptions = services
                    .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                services.Remove(dbContextOptions);

                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));

                services
                 .AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));

            });

            return base.CreateHost(builder);
        }
    }
}
