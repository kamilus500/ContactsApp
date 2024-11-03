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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateContactCommandHandler(IContactsRepository contactsRepository, 
            IMemoryCache memoryCache, 
            ILogger<UpdateContactCommandHandler> logger, 
            IHttpContextAccessor httpContextAccessor
        )
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
        }

        public async Task<Domain.Entities.Contact> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateContactCommandHandler handler execute {DateTime.UtcNow}");

            var updatedContact = request.Adapt<Domain.Entities.Contact>();

            var currentUser = _httpContextAccessor.HttpContext?.User;

            if (currentUser is null)
            {
                throw new ArgumentNullException(nameof(currentUser));
            }

            if (request.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);
                    updatedContact.Image = memoryStream.ToArray();
                }
            }

            updatedContact.UserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            await _contactsRepository.UpdateContact(updatedContact, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allContactsCacheKey);

            return updatedContact;
        }
    }
}
