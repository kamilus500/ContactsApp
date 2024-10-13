using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace ContactsApp.Application.Contact.Queries.GetContactById
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMemoryCache _memoryCache;
        private const string mainContactCacheKey = "ContactCacheKey";
        public GetContactByIdQueryHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            string cacheKey = $"{mainContactCacheKey}_{request.ContactId}";

            if (!_memoryCache.TryGetValue(cacheKey, out ContactDto contactDto))
            {
                var contact = await _contactsRepository.GetContactById(request.ContactId, cancellationToken);

                contactDto = contact.Adapt<ContactDto>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

                _memoryCache.Set(cacheKey, contactDto, cacheEntryOptions);
            }

            return contactDto;
        }
    }
}
