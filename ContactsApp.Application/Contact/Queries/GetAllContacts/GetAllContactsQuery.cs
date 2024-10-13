using ContactsApp.Domain.Dtos;
using MediatR;

namespace ContactsApp.Application.Contact.Queries.GetAllContacts
{
    public class GetAllContactsQuery : IRequest<IEnumerable<ContactDto>>
    {
        public string UserId { get; set; }
        public GetAllContactsQuery(string userId)
        {
            UserId = userId;
        }
    }
}
