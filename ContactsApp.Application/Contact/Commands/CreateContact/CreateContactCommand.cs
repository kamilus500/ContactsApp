using ContactsApp.Domain.Dtos;
using MediatR;

namespace ContactsApp.Application.Contact.Commands.CreateContact
{
    public class CreateContactCommand : CreateUpdateContactDto, IRequest<Domain.Entities.Contact>
    {
        
    }
}
