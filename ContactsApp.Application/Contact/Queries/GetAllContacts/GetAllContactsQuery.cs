using ContactsApp.Domain.Dtos;
using MediatR;

namespace ContactsApp.Application.Contact.Queries.GetAllContacts
{
    public class GetAllContactsQuery : IRequest<IEnumerable<ContactDto>>
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public GetAllContactsQuery(int take, int skip)
        {
            Take = take;
            Skip = skip;
        }
    }
}
