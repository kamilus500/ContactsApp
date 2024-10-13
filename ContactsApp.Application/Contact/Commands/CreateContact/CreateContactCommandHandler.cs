using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ContactsApp.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Domain.Entities.Contact>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMemoryCache _memoryCache;
        
        public CreateContactCommandHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<Domain.Entities.Contact> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var newContact = request.Adapt<Domain.Entities.Contact>();
            newContact.Id = Guid.NewGuid().ToString();
            await _contactsRepository.CreateContact(newContact, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allContactsCacheKey);

            return newContact;
        }
    }
}
