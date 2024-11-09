using Microsoft.AspNetCore.Http;

namespace ContactsApp.Application.CurrentUser
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
    }
}
