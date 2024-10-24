﻿using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contact.Queries.GetAllContacts
{
    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactDto>>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetAllContactsQueryHandler> _logger;

        public GetAllContactsQueryHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache, ILogger<GetAllContactsQueryHandler> logger)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<ContactDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
           _logger.LogInformation($"GetAllContactsQuerys handler execute {DateTime.UtcNow}");

            if (!_memoryCache.TryGetValue(CacheItemKeys.allContactsCacheKey, out IEnumerable<ContactDto> contactDtos))
            {
                var contacts = await _contactsRepository.GetContacts( cancellationToken);

                contactDtos = contacts.Adapt<IEnumerable<ContactDto>>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

                _memoryCache.Set(CacheItemKeys.allContactsCacheKey, contactDtos, cacheEntryOptions);
            }
            
            return contactDtos;
        }
    }
}
