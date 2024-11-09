using ContactsApp.Application.Contact.Commands.UpdateContact;
using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace ContactsApp.Application.CurrentUser.Commands
{
    public class UpdateCurrentUserCommandHandler : IRequestHandler<UpdateCurrentUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<UpdateContactCommandHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateCurrentUserCommandHandler(IUserRepository userRepository,
            IMemoryCache memoryCache,
            ILogger<UpdateContactCommandHandler> logger,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
        }

        public async Task<User> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
        {
            var userFromRequest = request.Adapt<User>();

            var currentUser = _httpContextAccessor.HttpContext?.User;

            if (currentUser is null)
            {
                throw new ArgumentNullException(nameof(currentUser));
            }

            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();

            userFromRequest.Id = userId;
            if (request.Image != null && request.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);
                    userFromRequest.Image = memoryStream.ToArray();
                }
            }

            var updatedUser = await _userRepository.UpdateUser(userFromRequest, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allContactsCacheKey);

            return updatedUser;
        }
    }
}
