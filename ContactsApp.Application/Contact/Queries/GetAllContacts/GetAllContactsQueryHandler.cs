using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contact.Queries.GetAllContacts
{
    public class GetAllContactsQueryHandler : BaseHandler<GetAllContactsQueryHandler> ,IRequestHandler<GetAllContactsQuery, IEnumerable<ContactDto>>
    {
        private readonly IContactsRepository _contactsRepository;

        public GetAllContactsQueryHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache, ILogger<GetAllContactsQueryHandler> logger) : base(memoryCache, logger)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        public async Task<IEnumerable<ContactDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"GetAllContactsQuerys handler execute {DateTime.UtcNow}");

            var cacheKey = $"{CacheItemKeys.allContactsCacheKey}_{request.Take}_{request.Skip}";
            var actuallCacheKey = CacheItemKeys.actualCacheKey;

            if (!actuallCacheKey.Contains(cacheKey, StringComparison.InvariantCulture) || !_memoryCache.TryGetValue(cacheKey, out IEnumerable<ContactDto> contactDtos))
            {
                var contacts = await _contactsRepository.GetContacts(request.Take, request.Skip, cancellationToken);

                contactDtos = contacts.Adapt<IEnumerable<ContactDto>>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(2))
                    .SetSize(50);

                _memoryCache.Set(cacheKey, contactDtos, cacheEntryOptions);
                CacheItemKeys.actualCacheKey = cacheKey;
            }
            return contactDtos;
        }
    }
}
