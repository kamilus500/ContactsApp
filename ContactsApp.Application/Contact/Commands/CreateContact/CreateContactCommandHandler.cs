using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;

namespace ContactsApp.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Domain.Entities.Contact>
    {
        private readonly IContactsRepository _contactsRepository;
        public CreateContactCommandHandler(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        public async Task<Domain.Entities.Contact> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var newContact = request.Adapt<Domain.Entities.Contact>();
            newContact.Id = Guid.NewGuid().ToString();
            await _contactsRepository.CreateContact(newContact, cancellationToken);

            return newContact;
        }
    }
}
