using ContactsApp.Application.Auth.Commands.Login;
using ContactsApp.Application.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost("/login")]
        public async Task<ActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);
            return Ok(result);
        }

        [HttpPost("/register")]
        public async Task<ActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);
            return Ok(result);
        }
    }
}
