using ContactsApp.API;
using ContactsApp.Domain.Entities;
using ContactsApp.Infrastructure.Persistance;
using ContactsApp.Tests.Configuration;
using ContactsApp.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;

namespace ContactsApp.Tests
{
    public class ContactsControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        public ContactsControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        private void SeedContacts(Contact contact)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                _dbContext.Contacts.Add(contact);
                _dbContext.SaveChanges();
            }
        }

        [Theory]
        [InlineData(5, 0)]
        public async Task GetAll_WithParameters_ReturnsOkResult_WithOneCount(int take, int skip)
        {
            var response = await _client.GetAsync("/GetContacts/" + take + "/" + skip);

            var json = await response.Content.ReadAsStringAsync();

            var result = System.Text.Json.JsonSerializer.Deserialize<List<Contact>>(json);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task Delete_Contact_ReturnsNoContent()
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "TestName",
                Email = "test@gmail.com",
                UserId = "1"
            };

            SeedContacts(contact);

            var response = await _client.DeleteAsync("/DeleteContact/" + contact.Id);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task Create_Contact_ReturnsOkWithCreatedContact()
        {
            var contact = new Contact()
            {
                FirstName = "TestName",
                LastName = "TestLastName",
                Email = "test@gmail.com",
                NumberPhone = "111222333",
                UserId = "1"
            };

            var httpContent = contact.ToFormDataHttpContent();

            var response = await _client.PostAsync("/CreateContact", httpContent);

            var responseJson = await response.Content.ReadAsStringAsync();

            var responseContact = JsonConvert.DeserializeObject<Contact>(responseJson);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("test@gmail.com", responseContact.Email);
        }

        [Fact]
        public async Task Update_Contact_ReturnsOkWithUpdatedContact()
        {
            string contactId = Guid.NewGuid().ToString();

            var contact = new Contact()
            {
                Id = contactId,
                FirstName = "TestName",
                LastName = "TestLastName",
                Email = "test@gmail.com",
                NumberPhone = "111222333",
                UserId = "1"
            };

            SeedContacts(contact);

            contact.Email = "test2@gmail.com";
            contact.NumberPhone = "999222111";

            var httpContent = contact.ToFormDataHttpContent();
            
            var response = await _client.PutAsync("/UpdateContact", httpContent);

            var responseJson = await response.Content.ReadAsStringAsync();

            var responseContact = JsonConvert.DeserializeObject<Contact>(responseJson);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("999222111", responseContact.NumberPhone);
            Assert.Equal("test2@gmail.com", responseContact.Email);
        }
    }
}
