namespace SkillSystem.Application.Common.Exceptions;

public class EntityNotFoundException : ApplicationException
{
    public EntityNotFoundException(string name, object key) : base($"Entity {name} with key {key} was not found.")
    {
    }
}