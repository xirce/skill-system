using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillSystem.IdentityServer4.Data;
using SkillSystem.IdentityServer4.Models;

namespace SkillSystem.IdentityServer4;

public class Startup
{
    private readonly IWebHostEnvironment environment;

    public Startup(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddDbContext<SkillSystemIdentityDbContext>(
            options => options.UseInMemoryDatabase("SkillSystemIdentity")
        );

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<SkillSystemIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryClients(Config.Clients)
            .AddDeveloperSigningCredential()
            .AddAspNetIdentity<ApplicationUser>();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.EnsureUsersSeeded();
        }

        app.UseStaticFiles();
        app.UseRouting();

        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
    }
}