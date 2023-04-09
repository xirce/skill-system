namespace SkillSystem.IdentityServer4.Models.Users;

public record BatchGetUsersRequest
{
    public Guid[] UserIds { get; init; }
}
