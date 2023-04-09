using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestEase.Implementation;
using SkillSystem.Client;
using SkillSystem.Infrastructure.Persistence;

namespace SkillSystem.FuncTests;

[TestFixture]
internal class BaseTest
{
    protected readonly ISkillSystemClient Client;
    protected readonly WebApplicationFactory<Program> Factory;

    public BaseTest()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => builder.ConfigureServices(ConfigureServices));
        var httpClient = Factory.CreateClient();
        httpClient.BaseAddress = new Uri(httpClient.BaseAddress!.AbsoluteUri + "api");
        Client = new SkillSystemClient(new Client.Core.Client(new Requester(httpClient)));
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<SkillSystemDbContext>));

        if (descriptor != null)
            services.Remove(descriptor);

        services.AddDbContext<SkillSystemDbContext>(options => options.UseInMemoryDatabase("TestDb"));
    }
}
