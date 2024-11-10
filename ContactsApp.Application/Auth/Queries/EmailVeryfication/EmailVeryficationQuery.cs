using MediatR;

namespace ContactsApp.Application.Auth.Queries.EmailVeryfication
{
    public class EmailVeryficationQuery : IRequest<bool>
    {
        public string Email { get; set; }
        public EmailVeryficationQuery(string email)
        {
            Email = email;
        }
    }
}
