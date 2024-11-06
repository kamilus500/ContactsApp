using ContactsApp.Domain.Entities;

namespace ContactsApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userId, CancellationToken cancellationToken);
        
        Task Update(User user, CancellationToken cancellationToken);
    }
}
