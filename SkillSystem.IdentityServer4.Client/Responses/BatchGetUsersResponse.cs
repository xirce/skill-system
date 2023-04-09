namespace SkillSystem.IdentityServer4.Client.Responses;

public class BatchGetUsersResponse
{
    public IReadOnlyCollection<User> Users { get; set; }
}
