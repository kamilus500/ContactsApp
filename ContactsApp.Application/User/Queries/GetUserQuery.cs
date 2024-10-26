using MediatR;

namespace ContactsApp.Application.User.Queries
{
    public class GetUserQuery : IRequest<CurrentUserDto>
    {
    }
}
