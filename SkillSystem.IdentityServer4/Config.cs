using IdentityServer4;
using IdentityServer4.Models;

namespace SkillSystem.IdentityServer4;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("SkillSystem.WebApi")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new("SkillSystem.WebApi")
            {
                Scopes = { "SkillSystem.WebApi" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                ClientId = "skill-system-web",
                ClientName = "Skill System Web",
                RequireClientSecret = false,
                RequirePkce = true,
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:4200/signin-oidc", "http://localhost:4200/refresh-token" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "SkillSystem.WebApi"
                },
                AllowedCorsOrigins = { "http://localhost:4200" },
            }
        };
}