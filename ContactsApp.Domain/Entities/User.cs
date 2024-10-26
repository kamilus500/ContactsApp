using Microsoft.AspNetCore.Identity;

namespace ContactsApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
