using ContactsApp.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Auth.Queries.EmailVeryfication
{
    public class EmailVeryficationQueryHandler : IRequestHandler<EmailVeryficationQuery, bool>
    {
        private readonly ILogger<EmailVeryficationQuery> _logger;
        private readonly IUserRepository _userRepository;
        public EmailVeryficationQueryHandler(ILogger<EmailVeryficationQuery> logger, IUserRepository userRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> Handle(EmailVeryficationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Email veryfication query handler {DateTime.Now}");

            return await _userRepository.EmailVeryfication(request.Email, cancellationToken);
        }
    }
}
