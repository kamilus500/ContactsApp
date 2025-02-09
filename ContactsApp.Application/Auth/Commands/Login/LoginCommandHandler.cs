﻿using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Auth.Commands.Login
{
    public class LoginCommandHandler : BaseHandler<LoginCommandHandler>, IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;
        public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<LoginCommandHandler> logger, IMemoryCache memoryCache, IConfiguration configuration, ITokenRepository tokenRepository) : base (memoryCache, logger)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"LoginCommand Handler at {DateTime.Now}");
            var loginResponse = new LoginResponse();

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                loginResponse.Token = _tokenRepository.GenerateToken(user);
                loginResponse.FullName = $"{user.FirstName} {user.LastName}";
                loginResponse.UserImage = user.Image;
            }

            return loginResponse;
        }
    }
}
