using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ContactsApp.Application.User.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, CurrentUserDto>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<CurrentUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;

            if (currentUser is null)
            {
                throw new ArgumentNullException(nameof(currentUser));
            }

            return new CurrentUserDto()
            {
                Email = currentUser.FindFirst(ClaimTypes.Name).Value.ToString()
            };

        }
    }
}
