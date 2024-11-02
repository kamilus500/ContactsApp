using MediatR;

namespace ContactsApp.Application.Auth.Commands.Login
{
    public class LoginCommand : LoginDto, IRequest<LoginResponse>
    {

    }
}
