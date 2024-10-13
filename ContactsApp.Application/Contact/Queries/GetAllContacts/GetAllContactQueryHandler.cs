using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;

namespace ContactsApp.Application.Contact.Queries.GetAllContacts
{
    public class GetAllContactQueryHandler : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactDto>>
    {
        private readonly IContactsRepository _contactsRepository;
        public GetAllContactQueryHandler(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        public async Task<IEnumerable<ContactDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _contactsRepository.GetContacts(cancellationToken);
            return contacts.Adapt<IEnumerable<ContactDto>>();
        }
    }
}
