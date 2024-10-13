using ContactsApp.Application.Mapper;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApp.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMapster();

            MapsterConfig.Configure();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        }
    }
}
