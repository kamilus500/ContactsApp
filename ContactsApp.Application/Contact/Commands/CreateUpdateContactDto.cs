using Microsoft.AspNetCore.Http;

namespace ContactsApp.Domain.Dtos
{
    public class CreateUpdateContactDto
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public IFormFile? Image { get; set; }
        public string? UserId { get; set; }
    }
}