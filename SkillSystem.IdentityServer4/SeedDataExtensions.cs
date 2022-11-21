using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillSystem.IdentityServer4.Data;
using SkillSystem.IdentityServer4.Models;

namespace SkillSystem.IdentityServer4;

internal static class SeedDataExtensions
{
    public static void EnsureUsersSeeded(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var dbContext = serviceProvider.GetRequiredService<SkillSystemIdentityDbContext>();
        var database = dbContext.Database;
        if (database.IsRelational())
            database.Migrate();

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user1 = new ApplicationUser
        {
            FirstName = "Юзер",
            LastName = "Юзеров",
            Email = "user@mail.ru",
            UserName = "user@mail.ru"
        };
        var user2 = new ApplicationUser
        {
            FirstName = "Админ",
            LastName = "Админов",
            Email = "admin@mail.ru",
            UserName = "admin@mail.ru"
        };

        SeedUser(userManager, user1, "User123.");
        SeedUser(userManager, user2, "Admin123.");
    }

    private static void SeedUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string password)
    {
        var presentUser = userManager.FindByEmailAsync(user.Email).Result;
        if (presentUser is null)
        {
            var identityResult = userManager.CreateAsync(user, password).Result;
            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.First().Description);
        }
    }
}