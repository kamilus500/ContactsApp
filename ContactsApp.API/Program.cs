using ContactsApp.Application.Extensions;
using ContactsApp.Domain.Entities;
using ContactsApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("connectionString") ?? throw new ArgumentNullException("Connection string is empty");

builder.Services.AddInfrastructure(builder);
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<User>();

app.AddMiddlewares(app.Environment);
app.MapControllers();

app.Run();
