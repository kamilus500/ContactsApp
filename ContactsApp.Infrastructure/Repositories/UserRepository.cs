using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Interfaces;
using ContactsApp.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUser(string userId, CancellationToken cancellationToken)
            => await _dbContext.Users.AsNoTracking().FirstAsync();

        public async Task<User> UpdateUser(User user, CancellationToken cancellationToken)
        {
            var curentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            curentUser.FirstName = user.FirstName;
            curentUser.LastName = user.LastName;
            curentUser.Email = user.Email;
            curentUser.Image = user.Image ?? curentUser.Image;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return curentUser;
        }
    }
}
