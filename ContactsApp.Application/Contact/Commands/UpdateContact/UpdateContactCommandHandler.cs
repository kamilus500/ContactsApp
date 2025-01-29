using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contact.Commands.UpdateContact
{
    public class UpdateContactCommandHandler : BaseHandler<UpdateContactCommandHandler>, IRequestHandler<UpdateContactCommand, Domain.Entities.Contact>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly ITokenRepository _tokenRepository;

        public UpdateContactCommandHandler(IContactsRepository contactsRepository, 
            IMemoryCache memoryCache, 
            ILogger<UpdateContactCommandHandler> logger, 
            ITokenRepository tokenRepository
        ) : base (memoryCache, logger)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
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
