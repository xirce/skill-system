namespace SkillSystem.IdentityServer4.Client.Responses;

public class SearchUsersResponse
{
    public IReadOnlyCollection<User> Items { get; set; }
    public Pagination Pagination { get; set; }
}
