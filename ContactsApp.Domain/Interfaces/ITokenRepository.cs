using ContactsApp.Domain.Entities;

namespace ContactsApp.Domain.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateToken(User user);
    }
}
