using ContactsApp.Application.Auth.Commands.Login;
using ContactsApp.Application.Auth.Commands.Register;
using ContactsApp.Application.Auth.Queries.EmailVeryfication;
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
        public async Task<ActionResult> Register([FromForm] RegisterCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);
            return Ok(result);
        }

        [HttpGet("/emailveryfication/{email}")]
        public async Task<ActionResult> EmailVerication([FromRoute] string email)
        {
            var result = await _mediator.Send(new EmailVeryficationQuery(email));
            return Ok(result);
        }
    }
}
