using FluentAssertions;

namespace SkillSystem.FuncTests;

internal class GetRoleShould : BaseTest
{
    [Test]
    public async Task GetRoleWithUnknownRoleId_ShouldReturnRoleNotFound()
    {
        var getRoleResult = await Client.GetRole(1);

        getRoleResult.IsSuccess.Should().BeFalse();
        getRoleResult.StatusCode.Should().Be(404);
    }
}
