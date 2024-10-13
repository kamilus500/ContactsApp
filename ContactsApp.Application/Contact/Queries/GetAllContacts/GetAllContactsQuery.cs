using ContactsApp.Domain.Dtos;
using MediatR;

namespace ContactsApp.Application.Contact.Queries.GetAllContacts
{
    public class GetAllContactsQuery : IRequest<IEnumerable<ContactDto>>
    {
    }
}
