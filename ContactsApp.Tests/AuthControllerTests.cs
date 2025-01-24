using ContactsApp.API;
using ContactsApp.Application.Auth.Commands;
using ContactsApp.Domain.Entities;
using ContactsApp.Infrastructure.Persistance;
using ContactsApp.Tests.Configuration;
using ContactsApp.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace ContactsApp.Tests
{
    public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
    {

        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        public AuthControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        private void SeedUser(User user)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
        }


        [Fact]
        public async Task RegisterUser_ReturnsOk_WithTrue()
        {
            var registerUser = new RegisterDto()
            {
                Email = "testEmail@gmail.com",
                FirstName = "testFirstName",
                LastName = "testLastName",
                Password = "qweQWE321#@!"
            };

            var httpContent = registerUser.ToFormDataHttpContent();

            var response = await _client.PostAsync("/register", httpContent);

            var result = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(Convert.ToBoolean(result));
        }

        [Fact]
        public async Task LoginUser_ReturnsOk_WithTokenAndUserName()
        {
            var registerUser = new RegisterDto()
            {
                Email = "test@gmail.com",
                FirstName = "testName",
                LastName = "testLastName",
                Password = "qweQWE321#@!"
            };

            var httpContent = registerUser.ToFormDataHttpContent();

            await _client.PostAsync("/register", httpContent);

            var loginUser = new LoginDto()
            {
                Email = "test@gmail.com",
                Password = "qweQWE321#@!"
            };

            httpContent = loginUser.ToJsonHttpContent();

            var response = await _client.PostAsync("/login", httpContent);

            var json = await response.Content.ReadAsStringAsync();

            var options = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = System.Text.Json.JsonSerializer.Deserialize<LoginResponse>(json, options);

            Assert.NotEmpty(result.Token);
            Assert.NotEmpty(result.FullName);
        }

        [Fact]
        public async Task EmailVeryfication_ForValidEmail_ReturnsTrue()
        {
            var user = new User()
            {
                Email = "test@gmail.com",
                Id = Guid.NewGuid().ToString()
            };

            SeedUser(user);

            var response = await _client.GetAsync($"/emailveryfication/test@gmail.com");

            var result = await response.Content.ReadAsStringAsync();

            Assert.True(Convert.ToBoolean(result));
        }

        [Fact]
        public async Task EmailVeryfication_ForInvalidEmail_ReturnsFalse()
        {
            var user = new User()
            {
                Email = "test@gmail.com",
                Id = Guid.NewGuid().ToString()
            };

            SeedUser(user);

            var response = await _client.GetAsync($"/emailveryfication/error@gmail.com");

            var result = await response.Content.ReadAsStringAsync();

            Assert.False(Convert.ToBoolean(result));
        }
    }
}
