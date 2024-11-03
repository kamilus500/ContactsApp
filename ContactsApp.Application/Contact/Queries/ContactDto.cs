namespace ContactsApp.Domain.Dtos
{
    public class ContactDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public byte[] Image { get; set; }
    }
}
