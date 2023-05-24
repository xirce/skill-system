using Mapster;
using SkillSystem.Application.Common.Extensions;
using SkillSystem.Application.Common.Services;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Salaries;
using SkillSystem.Application.Repositories.SalaryTransactions;
using SkillSystem.Application.Services.Salaries.Models;
using SkillSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace SkillSystem.Application.Services.SalariesTransactions;

public class SalariesTransactionsService : ISalariesTransactionsService
{
    private readonly ISalariesRepository salariesRepository;
    private readonly ISalaryTransactionsRepository transactionsRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ICurrentUserProvider currentUserProvider;

    public SalariesTransactionsService(ISalariesRepository salariesRepository,
        ISalaryTransactionsRepository transactionsRepository,
        IUnitOfWork unitOfWork, ICurrentUserProvider currentUserProvider)
    {
        this.salariesRepository = salariesRepository;
        this.transactionsRepository = transactionsRepository;
        this.unitOfWork = unitOfWork;
        this.currentUserProvider = currentUserProvider;
    }

    public async Task<int> SaveSalary(SalaryRequest request)
    {
        var currentUser = currentUserProvider.User;
        var userId = currentUser.GetUserId();
        if (userId == null)
            throw new ArgumentNullException();
        Guid changedBy = (Guid)userId;
        var salary = await SaveSalaryAsync(request);
        await SaveTransactionAsync(salary, changedBy);
        await unitOfWork.SaveChangesAsync();
        return salary.Id;
    }

    private async Task<Salary> SaveSalaryAsync(SalaryRequest request)
    {
        var newSalary = request.Adapt<Salary>();
        var lastSalary = await salariesRepository.FindSalaryByMonthAsync(newSalary.EmployeeId,
            newSalary.StartDate);
        var currentSalary = await salariesRepository.FindSalaryByMonthAsync(newSalary.EmployeeId, DateTime.UtcNow);
        if (currentSalary == null && (newSalary.StartDate.Month == DateTime.UtcNow.Month &&
            newSalary.StartDate.Year == DateTime.UtcNow.Year))
            return await salariesRepository.CreateSalaryAsync(newSalary);
        if (newSalary.StartDate < DateTime.UtcNow || (newSalary.StartDate.Month == DateTime.UtcNow.Month &&
            newSalary.StartDate.Year == DateTime.UtcNow.Year))
            throw new ValidationException($"Access is denied to save a salary with a date {newSalary.StartDate}");
        if (lastSalary != null && lastSalary.StartDate.Month == newSalary.StartDate.Month
            && lastSalary.StartDate.Year == newSalary.StartDate.Year)
        {
            lastSalary.Wage = newSalary.Wage;
            lastSalary.Rate = newSalary.Rate;
            lastSalary.Bonus = newSalary.Bonus;
            lastSalary.StartDate = newSalary.StartDate;
            return await salariesRepository.UpdateSalaryAsync(lastSalary);
        }
        else
            return await salariesRepository.CreateSalaryAsync(newSalary);
    }

    private async Task SaveTransactionAsync(Salary salary, Guid changedBy)
    {
        var transaction = new SalaryTransaction
        {
            EmployeeId = salary.EmployeeId,
            ChangedBy = changedBy,
            SalaryChangeDate = DateTime.UtcNow,
            Wage = salary.Wage,
            Bonus = salary.Bonus,
            Rate = salary.Rate,
            StartDate = salary.StartDate
        };
        await transactionsRepository.CreateTransactionAsync(transaction);
    }
}
