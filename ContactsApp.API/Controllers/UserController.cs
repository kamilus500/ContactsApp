using ContactsApp.Application.CurrentUser.Commands;
using ContactsApp.Application.CurrentUser.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet("/GetCurrentUser")]
        public async Task<ActionResult> GetCurrentUser()
            => Ok(await _mediator.Send(new GetUserQuery()));

        [HttpPut("/UpdateCurrentUser")]
        public async Task<ActionResult> UpdateCurrentUser([FromForm] UpdateCurrentUserCommand userCommand)
            => Ok(await _mediator.Send(userCommand));
    }
}
