using ContactsApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, bool>
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterCommandHandler> _logger;
        public RegisterCommandHandler(UserManager<User> userManager, ILogger<RegisterCommandHandler> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            using (var memoryStream = new MemoryStream())
            {
                await request.Image.CopyToAsync(memoryStream);
                newUser.Image = memoryStream.ToArray();
            }

            var result = await _userManager.CreateAsync(newUser, request.Password);

            return result.Succeeded;
        }
    }
}
