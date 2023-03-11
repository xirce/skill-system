using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace SkillSystem.IdentityServer4;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new("roles", "Roles", new[] { JwtClaimTypes.Role })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("SkillSystem.WebApi")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new("SkillSystem.WebApi", "Skill System Web Api", new[] { JwtClaimTypes.Role })
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
                PostLogoutRedirectUris = { "http://localhost:4200" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "SkillSystem.WebApi",
                    "roles"
                },
                AllowedCorsOrigins = { "http://localhost:4200" },
            },
            new()
            {
                ClientId = "skill-system-swagger",
                ClientName = "Skill System Swagger",
                RequireClientSecret = false,
                RequirePkce = true,
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris =
                {
                    "https://localhost:8000/swagger/oauth2-redirect.html",
                    "http://localhost:8001/swagger/oauth2-redirect.html"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "SkillSystem.WebApi",
                    "roles"
                },
                AllowedCorsOrigins = { "https://localhost:8000", "http://localhost:8001" },
            }
        };
}
