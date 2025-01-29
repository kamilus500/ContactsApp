using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : BaseHandler<RegisterCommandHandler>, IRequestHandler<RegisterCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        public RegisterCommandHandler(UserManager<User> userManager, IMemoryCache memoryCache, ILogger<RegisterCommandHandler> logger) : base(memoryCache, logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Register handler {DateTime.UtcNow}");

            var newUser = new User()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email
            };

            if (request.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.Image.CopyToAsync(memoryStream);
                    newUser.Image = memoryStream.ToArray();
                }
            }

            var result = await _userManager.CreateAsync(newUser, request.Password);

            return result.Succeeded;
        }
    }
}
