using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SkillSystem.IdentityServer4.Data.Entities;

namespace SkillSystem.IdentityServer4;

public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public UserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
    }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim(JwtClaimTypes.GivenName, user.FirstName));
        identity.AddClaim(new Claim(JwtClaimTypes.FamilyName, user.LastName));
        identity.AddClaim(new Claim(JwtClaimTypes.MiddleName, user.Patronymic));

        var nameClaim = identity.FindFirst(claim => claim.Type == JwtClaimTypes.Name);
        identity.RemoveClaim(nameClaim);
        identity.AddClaim(new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

        return identity;
    }
}
