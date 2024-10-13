using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Interfaces;
using ContactsApp.Infrastructure.Persistance;
using ContactsApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApp.Infrastructure.Extensions
{
    public static class ServiceCollecionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

            services.AddAuthorizationBuilder();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

            services.AddMemoryCache();

            services.AddTransient<IContactsRepository, ContactsRepository>();
        }
    }
}
