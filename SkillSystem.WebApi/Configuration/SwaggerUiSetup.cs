using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SkillSystem.WebApi.Configuration;

public class SwaggerUiSetup : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        options.OAuthClientId("skill-system-swagger");
        options.OAuthScopes(
            OpenIdConnectScope.OpenId,
            OpenIdConnectScope.OpenIdProfile,
            "SkillSystem.WebApi",
            "roles");
        options.OAuthUsePkce();
    }
}
