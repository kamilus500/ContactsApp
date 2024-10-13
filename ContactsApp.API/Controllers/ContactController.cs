using ContactsApp.Application.Contact.Commands.CreateContact;
using ContactsApp.Application.Contact.Queries.GetAllContacts;
using ContactsApp.Application.Contact.Queries.GetContactById;
using ContactsApp.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet("/GetContacts")]
        public async Task<ActionResult> GetContacts()
            => Ok(await _mediator.Send(new GetAllContactsQuery()));

        [HttpGet("/GetContactById/{id}")]
        public async Task<ActionResult> GetContactById([FromRoute]string id)
        {
            var contact = await _mediator.Send(new GetContactByIdQuery(id));

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult> CreateContact([FromBody] CreateContactCommand createContactCommand)
        {
            var createdContact = await _mediator.Send(createContactCommand);
            return Created($"/CreateContact/{createdContact.Id}", createdContact);
        }
            
    }
}
