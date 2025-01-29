using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Contact.Queries.GetContactById
{
    public class GetContactByIdQueryHandler : BaseHandler<GetContactByIdQueryHandler>,IRequestHandler<GetContactByIdQuery, ContactDto>
    {
        private readonly IContactsRepository _contactsRepository;
        public GetContactByIdQueryHandler(IContactsRepository contactsRepository, IMemoryCache memoryCache, ILogger<GetContactByIdQueryHandler> logger) : base(memoryCache, logger)
        {
            _contactsRepository = contactsRepository ?? throw new ArgumentNullException(nameof(contactsRepository));
        }

        public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetContactByIdQuery handler {request.ContactId} handler execute");

            string cacheKey = $"{CacheItemKeys.mainContactCacheKey}_{request.ContactId}";

            if (!_memoryCache.TryGetValue(cacheKey, out ContactDto contactDto))
            {
                var contact = await _contactsRepository.GetContactById(request.ContactId, cancellationToken);

                contactDto = contact.Adapt<ContactDto>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(3))
                    .SetSize(50);

                _memoryCache.Set(cacheKey, contactDto, cacheEntryOptions);
            }

            return contactDto;
        }
    }
}
