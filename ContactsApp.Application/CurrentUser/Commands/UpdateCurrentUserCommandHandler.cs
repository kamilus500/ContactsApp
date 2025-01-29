using ContactsApp.Application.BaseClasses;
using ContactsApp.Application.Contact.Commands.UpdateContact;
using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Global;
using ContactsApp.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.CurrentUser.Commands
{
    public class UpdateCurrentUserCommandHandler : BaseHandler<UpdateContactCommandHandler>, IRequestHandler<UpdateCurrentUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public UpdateCurrentUserCommandHandler(IUserRepository userRepository,
            IMemoryCache memoryCache,
            ILogger<UpdateContactCommandHandler> logger,
            ITokenRepository tokenRepository
        ) : base (memoryCache, logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        public async Task<User> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Update current user handler {DateTime.Now}");

            var userFromRequest = request.Adapt<User>();

            var userId = _tokenRepository.GetUserId();

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
