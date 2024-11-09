using ContactsApp.Domain.Entities;

namespace ContactsApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userId, CancellationToken cancellationToken);
        
        Task<User> UpdateUser(User user, CancellationToken cancellationToken);
    }
}
