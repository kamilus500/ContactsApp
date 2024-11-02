
using ContactsApp.Domain.Entities;
using MediatR;

namespace ContactsApp.Application.Auth.Commands.Register
{
    public class RegisterCommand : RegisterDto, IRequest<bool>
    {
    }
}
