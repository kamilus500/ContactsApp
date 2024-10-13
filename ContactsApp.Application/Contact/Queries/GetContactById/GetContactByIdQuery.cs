using ContactsApp.Domain.Dtos;
using MediatR;

namespace ContactsApp.Application.Contact.Queries.GetContactById
{
    public class GetContactByIdQuery : IRequest<ContactDto>
    {
        public string ContactId { get; set; }
        public GetContactByIdQuery(string contactId)
        {
            ContactId = contactId;
        }
    }
}
