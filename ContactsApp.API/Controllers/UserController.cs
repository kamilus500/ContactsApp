using ContactsApp.Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentException(nameof(mediator));
        }

        [HttpGet("/GetUser")]
        public async Task<ActionResult> GetUser()
        {
            var user = await _mediator.Send(new GetUserQuery());

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
