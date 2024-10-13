using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MapsterMapper;
using MediatR;

namespace ContactsApp.Application.Contact.Queries.GetContactById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto>
    {
        private readonly IContactsRepository _contactsRepository;
        public GetContactByIdQueryHandler(IContactsRepository contactsRepository, IMapper mapper)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _contactsRepository.GetContactById(request.ContactId, cancellationToken);

            return contact.Adapt<ContactDto>();
        }
    }
}
