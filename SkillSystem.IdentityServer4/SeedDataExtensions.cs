using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillSystem.IdentityServer4.Data;
using SkillSystem.IdentityServer4.Data.Entities;

namespace SkillSystem.IdentityServer4;

internal static class SeedDataExtensions
{
    public static void EnsureUsersSeeded(this IApplicationBuilder applicationBuilder)
    {
        var scope = applicationBuilder.ApplicationServices.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var dbContext = serviceProvider.GetRequiredService<SkillSystemIdentityDbContext>();
        var database = dbContext.Database;
        if (database.IsRelational())
            database.Migrate();

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var user1 = new ApplicationUser
        {
            FirstName = "Юзер",
            LastName = "Юзеров",
            Patronymic = "Юзерович",
            Email = "user@mail.ru",
            UserName = "user@mail.ru"
        };
        var user2 = new ApplicationUser
        {
            FirstName = "Админ",
            LastName = "Админов",
            Patronymic = "Админович",
            Email = "admin@mail.ru",
            UserName = "admin@mail.ru"
        };

        const string adminRoleName = "admin";
        SeedRole(roleManager, adminRoleName);

        SeedUser(userManager, user1, "User123.");
        SeedUser(userManager, user2, "Admin123.");

        user2 = userManager.FindByNameAsync(user2.UserName).GetAwaiter().GetResult();
        EnsureUserInRole(userManager, user2, adminRoleName);
    }

    private static void SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
    {
        var presentRole = roleManager.FindByNameAsync(roleName).GetAwaiter().GetResult();
        if (presentRole is null)
        {
            var identityResult = roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.First().Description);
        }
    }

    private static void SeedUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string password)
    {
        var presentUser = userManager.FindByEmailAsync(user.Email).GetAwaiter().GetResult();
        if (presentUser is null)
        {
            var identityResult = userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.First().Description);
        }
    }

    private static void EnsureUserInRole(
        UserManager<ApplicationUser> userManager,
        ApplicationUser user2,
        string roleName)
    {
        if (!userManager.IsInRoleAsync(user2, roleName).GetAwaiter().GetResult())
        {
            var identityResult = userManager.AddToRoleAsync(user2, roleName).GetAwaiter().GetResult();
            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.First().Description);
        }
    }
}
