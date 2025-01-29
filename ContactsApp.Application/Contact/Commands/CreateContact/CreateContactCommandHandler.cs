using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommandHandler : BaseHandler<CreateContactCommandHandler>, IRequestHandler<CreateContactCommand, Domain.Entities.Contact>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly ITokenRepository _tokenRepository;
        
        public CreateContactCommandHandler(IContactsRepository contactsRepository, 
            IMemoryCache memoryCache, 
            ILogger<CreateContactCommandHandler> logger, 
            ITokenRepository tokenRepository
        ) : base(memoryCache, logger)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        public async Task<Domain.Entities.Contact> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateContactCommandHandler handler execute {DateTime.UtcNow}");

            var newContact = request.Adapt<Domain.Entities.Contact>();
            newContact.Id = Guid.NewGuid().ToString();

            newContact.UserId = _tokenRepository.GetUserId();

            if (request.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);
                    newContact.Image = memoryStream.ToArray();
                }
            }

            await _contactsRepository.CreateContact(newContact, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.actualCacheKey);
            CacheItemKeys.actualCacheKey = string.Empty;
            return newContact;
        }
    }
}
