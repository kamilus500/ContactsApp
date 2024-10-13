using Microsoft.AspNetCore.Identity;

namespace ContactsApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public IEnumerable<Contact> Contacts { get; set; }
    }
}
