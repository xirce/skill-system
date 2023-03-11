namespace SkillSystem.Application.Common.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException()
    {
    }

    public ForbiddenException(string message) : base(message)
    {
    }
}
