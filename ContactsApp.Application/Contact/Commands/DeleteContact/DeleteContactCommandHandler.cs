using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contact.Commands.DeleteContact
{
    public class DeleteContactCommandHandler : BaseHandler<DeleteContactCommandHandler>, IRequestHandler<DeleteContactCommand>
    {
        private readonly IContactsRepository _contactRepository;
        public DeleteContactCommandHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache, ILogger<DeleteContactCommandHandler> logger) : base (memoryCache, logger)
        {
            _contactRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
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
