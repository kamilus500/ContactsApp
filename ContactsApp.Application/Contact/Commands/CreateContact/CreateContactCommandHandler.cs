using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ContactsApp.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Domain.Entities.Contact>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ITokenRepository _tokenRepository;
        private ILogger<CreateContactCommandHandler> _logger;
        
        public CreateContactCommandHandler(IContactsRepository contactsRepository, 
            IMemoryCache memoryCache, 
            ILogger<CreateContactCommandHandler> logger, 
            ITokenRepository tokenRepository
        )
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        public async Task<Domain.Entities.Contact> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateContactCommandHandler handler execute {DateTime.UtcNow}");

            var newContact = request.Adapt<Domain.Entities.Contact>();
            newContact.Id = Guid.NewGuid().ToString();

            newContact.UserId = _tokenRepository.GetUserId();

            using (var memoryStream = new MemoryStream())
            {
                await request.Image.CopyToAsync(memoryStream);
                newContact.Image = memoryStream.ToArray();
            }

            await _contactsRepository.CreateContact(newContact, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.actualCacheKey);
            CacheItemKeys.actualCacheKey = string.Empty;
            return newContact;
        }
    }
}
