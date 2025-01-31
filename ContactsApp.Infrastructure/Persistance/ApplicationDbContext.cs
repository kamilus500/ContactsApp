using ContactsApp.Domain.Entities;
using ContactsApp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactsApp.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        private readonly ITokenRepository _tokenRepository;

        public DbSet<Contact> Contacts { get; set; }
        public ApplicationDbContext(DbContextOptions options, ITokenRepository tokenRepository) : base(options)
        {
            _tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>()
                .HasQueryFilter(x => x.UserId == _tokenRepository.GetUserId());

            builder.Entity<Contact>()
                .HasOne(x => x.User)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
