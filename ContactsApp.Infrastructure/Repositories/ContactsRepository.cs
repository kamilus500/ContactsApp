using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Interfaces;
using ContactsApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Infrastructure.Repositories
{
    public class ContactsRepository : BaseRepository, IContactsRepository
    {
        public ContactsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateContact(Contact contact, CancellationToken cancellationToken)
        {
            await _dbContext.Contacts.AddAsync(contact, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);  
        }

        public async Task DeleteContact(string contactId, CancellationToken cancellationToken)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == contactId, cancellationToken);
            _dbContext.Contacts.Remove(contact);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Contact> GetContactById(string contactId, CancellationToken cancellationToken)
            => await _dbContext.Contacts
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == contactId, cancellationToken);

        public async Task<IEnumerable<Contact>> GetContacts(CancellationToken cancellationToken)
         => await _dbContext.Contacts
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

        public async Task<Contact> UpdateContact(Contact contact, CancellationToken cancellationToken)
        {
            _dbContext.Contacts.Update(contact);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return contact;
        }
    }
}
