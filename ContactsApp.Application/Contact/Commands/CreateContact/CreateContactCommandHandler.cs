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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ILogger<CreateContactCommandHandler> _logger;
        
        public CreateContactCommandHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache, ILogger<CreateContactCommandHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<Domain.Entities.Contact> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateContactCommandHandler handler execute {DateTime.UtcNow}");

            var newContact = request.Adapt<Domain.Entities.Contact>();
            newContact.Id = Guid.NewGuid().ToString();

            var currentUser = _httpContextAccessor.HttpContext?.User;

            if (currentUser is null)
            {
                throw new ArgumentNullException(nameof(currentUser));
            }

            newContact.UserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            await _contactsRepository.CreateContact(newContact, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allContactsCacheKey);

            return newContact;
        }
    }
}
