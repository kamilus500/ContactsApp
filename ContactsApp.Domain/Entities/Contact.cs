namespace ContactsApp.Domain.Entities
{
    public class Contact
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        //public byte[] Photo { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }
}
