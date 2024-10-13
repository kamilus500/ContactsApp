using ContactsApp.Domain.Dtos;
using MediatR;

namespace ContactsApp.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommand : CreateContactDto, IRequest<Domain.Entities.Contact>
    {
        
    }
}
