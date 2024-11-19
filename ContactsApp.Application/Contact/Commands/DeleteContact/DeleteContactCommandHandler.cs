using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contact.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
    {
        private readonly IContactsRepository _contactRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<DeleteContactCommandHandler> _logger;
        public DeleteContactCommandHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache, ILogger<DeleteContactCommandHandler> logger)
        {
            _contactRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(_memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DeleteContactCommand handler execute {DateTime.UtcNow}");
            await _contactRepository.DeleteContact(request.ContactId, cancellationToken);

            _memoryCache.Remove($"{CacheItemKeys.mainContactCacheKey}_{request.ContactId}");
            CacheItemKeys.actualCacheKey = string.Empty;
        }
    }
}
