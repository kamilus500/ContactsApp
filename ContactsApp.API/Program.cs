using ContactsApp.Application.Extensions;
using ContactsApp.Infrastructure.Extensions;

namespace ContactsApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure(builder);

            builder.Services.AddApplication();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowSpecificOrigins");

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.AddMiddlewares(app.Environment);
            app.MapControllers();

            app.Run();
        }
    }
}