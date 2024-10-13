namespace ContactsApp.Domain.Dtos
{
    public class CreateContactDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public string UserId { get; set; }
    }
}
