using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ContactsApp.Application.Contact.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Domain.Entities.Contact>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMemoryCache _memoryCache;

        public UpdateContactCommandHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<Domain.Entities.Contact> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var updatedContact = request.Adapt<Domain.Entities.Contact>();

            await _contactsRepository.UpdateContact(updatedContact, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allContactsCacheKey);

            return updatedContact;
        }
    }
}
