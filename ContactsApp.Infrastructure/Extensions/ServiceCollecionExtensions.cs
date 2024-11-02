using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Interfaces;
using ContactsApp.Infrastructure.Persistance;
using ContactsApp.Infrastructure.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace ContactsApp.Infrastructure.Extensions
{
    public static class ServiceCollecionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("connectionString") ?? throw new ArgumentNullException("Connection string is empty");
            var configuration = builder.Configuration;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            builder.Host.UseSerilog();

            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:JwtKey"])),
                };
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentityCore<User>()
                .AddUserManager<Microsoft.AspNetCore.Identity.UserManager<User>>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMemoryCache();

            services.AddTransient<IContactsRepository, ContactsRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
        }
    }
}
