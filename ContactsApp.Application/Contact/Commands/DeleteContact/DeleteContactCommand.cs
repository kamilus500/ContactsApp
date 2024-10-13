using MediatR;

namespace ContactsApp.Application.Contact.Commands.DeleteContact
{
    public class DeleteContactCommand : IRequest
    {
        public string ContactId { get; set; }
        public DeleteContactCommand(string contactId)
        {
            ContactId = contactId;
        }
    }
}
