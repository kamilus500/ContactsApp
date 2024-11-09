using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading;

namespace ContactsApp.Application.CurrentUser.Queries.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetUserQueryHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public GetUserQueryHandler(IMemoryCache memoryCache, ILogger<GetUserQueryHandler> logger, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUser query handler {DateTime.Now}");

            var id = _tokenRepository.GetUserId();

            var user = await _userRepository.GetUser(id, cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Adapt<UserDto>();
        }
    }
}
