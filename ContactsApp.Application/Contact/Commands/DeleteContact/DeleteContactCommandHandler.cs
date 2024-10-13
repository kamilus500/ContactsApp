using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ContactsApp.Application.Contact.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
    {
        private readonly IContactsRepository _contactRepository;
        private readonly IMemoryCache _memoryCache;
        public DeleteContactCommandHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache)
        {
            _contactRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(_memoryCache));
        }

        public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            await _contactRepository.DeleteContact(request.ContactId, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allContactsCacheKey);
            _memoryCache.Remove($"{CacheItemKeys.mainContactCacheKey}_{request.ContactId}");
        }
    }
}
