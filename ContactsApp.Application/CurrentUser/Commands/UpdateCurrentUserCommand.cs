using ContactsApp.Domain.Entities;
using MediatR;

namespace ContactsApp.Application.CurrentUser.Commands
{
    public class UpdateCurrentUserCommand: UserDto, IRequest<User>
    {
    }
}
