namespace SkillSystem.IdentityServer4.Client.Requests;

public class BatchGetUsersRequest
{
    public Guid[] UserIds { get; }

    public BatchGetUsersRequest(Guid[] userIds)
    {
        UserIds = userIds;
    }
}
