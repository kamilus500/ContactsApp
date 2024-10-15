using ContactsApp.Domain.Dtos;
using MediatR;

namespace ContactsApp.Application.Contact.Commands.UpdateContact
{
    public class UpdateContactCommand : CreateUpdateContactDto, IRequest<Domain.Entities.Contact>
    {
    }
}
