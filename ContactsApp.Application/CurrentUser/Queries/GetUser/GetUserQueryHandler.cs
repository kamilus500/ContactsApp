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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserQueryHandler(IMemoryCache memoryCache, ILogger<GetUserQueryHandler> logger, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetUser query handler {DateTime.Now}");

            var currentUser = _httpContextAccessor.HttpContext?.User;

            if (currentUser is null)
            {
                throw new ArgumentNullException(nameof(currentUser));
            }

            var id = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            var user = await _userRepository.GetUser(id, cancellationToken);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.Adapt<UserDto>();
        }
    }
}
