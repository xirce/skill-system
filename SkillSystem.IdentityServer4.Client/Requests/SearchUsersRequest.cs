namespace SkillSystem.IdentityServer4.Client.Requests;

public class SearchUsersRequest
{
    public string? Query { get; set; }
    public int Offset { get; set; }
    public int Count { get; set; }
}
