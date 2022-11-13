namespace SkillSystem.IdentityServer4;

public class Startup
{
    private IWebHostEnvironment Environment { get; }

    public Startup(IWebHostEnvironment environment)
    {
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // uncomment, if you want to add an MVC-based UI
        // services.AddControllersWithViews();

        services.AddIdentityServer()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryClients(Config.Clients)
            .AddDeveloperSigningCredential();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add MVC
        // app.UseStaticFiles();
        // app.UseRouting();

        app.UseIdentityServer();

        // uncomment, if you want to add MVC
        // app.UseAuthorization();
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapDefaultControllerRoute();
        // });
    }
}