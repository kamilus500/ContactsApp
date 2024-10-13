using ContactsApp.Infrastructure.Persistance;

namespace ContactsApp.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        protected BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
