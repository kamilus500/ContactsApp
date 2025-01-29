using ContactsApp.Application.BaseClasses;
using ContactsApp.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ContactsApp.Application.Auth.Queries.EmailVeryfication
{
    public class EmailVeryficationQueryHandler : BaseHandler<EmailVeryficationQueryHandler>, IRequestHandler<EmailVeryficationQuery, bool>
    {
        private readonly IUserRepository _userRepository;
        public EmailVeryficationQueryHandler(ILogger<EmailVeryficationQueryHandler> logger, IUserRepository userRepository) : base(null, logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<bool> Handle(EmailVeryficationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Email veryfication query handler {DateTime.Now}");

            return await _userRepository.EmailVeryfication(request.Email, cancellationToken);
        }
    }
}
