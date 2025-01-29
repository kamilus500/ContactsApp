using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Dtos;
using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using ContactsApp.Infrastructure.Repositories;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.CurrentUser.Queries.GetUser
{
    public class GetUserQueryHandler : BaseHandler<GetUserQueryHandler>, IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public GetUserQueryHandler(IMemoryCache memoryCache, ILogger<GetUserQueryHandler> logger, IUserRepository userRepository, ITokenRepository tokenRepository) : base(memoryCache, logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUser query handler {DateTime.Now}");

            var id = _tokenRepository.GetUserId();

            string cacheKey = $"{CacheItemKeys.userCacheKey}_{id}";

            if (!_memoryCache.TryGetValue(cacheKey, out UserDto userDto))
            {
                var user = await _userRepository.GetUser(id, cancellationToken);

                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                userDto = user.Adapt<UserDto>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(3))
                    .SetSize(50);

                _memoryCache.Set(cacheKey, userDto, cacheEntryOptions);
            }

            return userDto;

        }
    }
}
