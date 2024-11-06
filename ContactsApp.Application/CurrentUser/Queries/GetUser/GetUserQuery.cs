using MediatR;

namespace ContactsApp.Application.CurrentUser.Queries.GetUser
{
    public class GetUserQuery : IRequest<UserDto>
    {
    }
}
