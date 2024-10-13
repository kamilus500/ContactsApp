using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ContactsApp.Application.Contact.Queries.GetAllContacts
{
    public class GetAllContactQueryHandler : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactDto>>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMemoryCache _memoryCache;
        private const string allContactsCacheKey = "AllContactsCacheKey";

        public GetAllContactQueryHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<IEnumerable<ContactDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            if (!_memoryCache.TryGetValue(allContactsCacheKey, out IEnumerable<ContactDto> contactDtos))
            {
                var contacts = await _contactsRepository.GetContacts(cancellationToken);

                contactDtos = contacts.Adapt<IEnumerable<ContactDto>>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

                _memoryCache.Set(allContactsCacheKey, contactDtos, cacheEntryOptions);
            }
            
            return contactDtos;
        }
    }
}
