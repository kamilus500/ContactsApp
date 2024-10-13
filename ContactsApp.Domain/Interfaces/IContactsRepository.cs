using ContactsApp.Domain.Entities;

namespace ContactsApp.Domain.Interfaces
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetContacts(string userId, CancellationToken cancellationToken);

        Task<Contact> GetContactById(string contactId, CancellationToken cancellationToken);

        Task CreateContact(Contact contact, CancellationToken cancellationToken);

        Task DeleteContact(string contactId, CancellationToken cancellationToken);
    }
}
