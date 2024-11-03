namespace ContactsApp.Application.Auth.Commands
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string FullName { get; set; }
        public byte[] UserImage { get; set; }
    }
}
