using SkillSystem.IdentityServer4.Data.Entities;

namespace SkillSystem.IdentityServer4.Models.Users;

public record UserModel
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Patronymic { get; init; }

    public static UserModel FromApplicationUser(ApplicationUser applicationUser)
    {
        return new UserModel
        {
            Id = applicationUser.Id,
            FirstName = applicationUser.FirstName,
            LastName = applicationUser.LastName,
            Patronymic = applicationUser.Patronymic
        };
    }
}