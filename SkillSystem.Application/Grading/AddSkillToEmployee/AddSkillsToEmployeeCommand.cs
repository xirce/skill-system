using MediatR;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Skills;
using SkillSystem.Application.Services.EmployeeSkills;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Grading.AddSkillToEmployee;

public record AddSkillsToEmployeeCommand(Guid EmployeeId, IReadOnlyCollection<int> SkillIds) : IRequest;

public class AddSkillToEmployeeCommandHandler : IRequestHandler<AddSkillsToEmployeeCommand>
{
    private readonly IEmployeeSkillsRepository employeeSkillsRepository;
    private readonly ISkillsReadOnlyRepository skillsRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IPublisher publisher;

    public AddSkillToEmployeeCommandHandler(
        IEmployeeSkillsRepository employeeSkillsRepository,
        ISkillsReadOnlyRepository skillsRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        this.employeeSkillsRepository = employeeSkillsRepository;
        this.skillsRepository = skillsRepository;
        this.unitOfWork = unitOfWork;
        this.publisher = publisher;
    }

    public async Task Handle(AddSkillsToEmployeeCommand request, CancellationToken cancellationToken)
    {
        var skillsToAdd = new List<EmployeeSkill>();
        foreach (var skillId in request.SkillIds)
            skillsToAdd.AddRange(await GetSkillsToAddAsync(request.EmployeeId, skillId));

        await employeeSkillsRepository.AddEmployeeSkillsAsync(skillsToAdd);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new EmployeeReceivedSkillsEvent(skillsToAdd), cancellationToken);
    }

    private async Task<IReadOnlyCollection<EmployeeSkill>> GetSkillsToAddAsync(Guid employeeId, int skillId)
    {
        return (await skillsRepository.TraverseSkillAsync(skillId)).ToArray()
            .Select(subSkill => EmployeeSkills.Received(employeeId, subSkill.Id))
            .ToArray();
    }
}

public record EmployeeReceivedSkillsEvent(IReadOnlyCollection<EmployeeSkill> ReceivedSkills);
