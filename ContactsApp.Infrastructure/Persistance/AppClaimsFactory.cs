using ContactsApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ContactsApp.Infrastructure.Persistance
{
    public class AppClaimsFactory : IUserClaimsPrincipalFactory<User>
    {
        public async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var claims = new Claim[] {
              new Claim(ClaimTypes.Name, user.UserName),
              new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Bearer");

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            return await Task.FromResult(claimsPrincipal);
        }
    }
}
