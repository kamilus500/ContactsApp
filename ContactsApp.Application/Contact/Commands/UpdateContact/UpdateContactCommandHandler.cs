using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ContactsApp.Application.Contact.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Domain.Entities.Contact>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<UpdateContactCommandHandler> _logger;
        private readonly ITokenRepository _tokenRepository;

        public UpdateContactCommandHandler(IContactsRepository contactsRepository, 
            IMemoryCache memoryCache, 
            ILogger<UpdateContactCommandHandler> logger, 
            ITokenRepository tokenRepository
        )
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        public async Task<Domain.Entities.Contact> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateContactCommandHandler handler execute {DateTime.UtcNow}");

            var updatedContact = request.Adapt<Domain.Entities.Contact>();

            if (request.Image != null && request.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);
                    updatedContact.Image = memoryStream.ToArray();
                }
            }
            else
            {
                var contact = await _contactsRepository.GetContactById(updatedContact.Id, cancellationToken);
                updatedContact.Image = contact.Image;
            }

            updatedContact.UserId = _tokenRepository.GetUserId();

            await _contactsRepository.UpdateContact(updatedContact, cancellationToken);
            _memoryCache.Remove(CacheItemKeys.actualCacheKey);
            CacheItemKeys.actualCacheKey = string.Empty;

            return updatedContact;
        }
    }
}
