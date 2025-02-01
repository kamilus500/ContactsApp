using ContactsApp.Application.Contact.Commands.CreateContact;
using ContactsApp.Application.Mapper;
using ContactsApp.Application.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApp.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlingMiddleware>();

            services.AddMapster();

            MapsterConfig.Configure();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            
            services.AddValidatorsFromAssemblyContaining<CreateContactCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();            
        }

        public static void AddMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();  
        }
    }
}
