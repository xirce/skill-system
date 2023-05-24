using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.SalaryTransactions;
using SkillSystem.Application.Services.SalaryTransactions.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/salary-transactions")]
public class SalaryTransactionsController : BaseController
{
    private readonly ISalaryTransactionsService salaryTransactionsService;

    public SalaryTransactionsController(ISalaryTransactionsService salaryTransactionsService)
    {
        this.salaryTransactionsService = salaryTransactionsService;
    }

    /// <summary>
    /// Получить информацию о зарплатных транзакциях.
    /// </summary>
    /// <param name="from">дата месяца, начиная с которого  требуется получить информацию о зарплатных транзациях</param>
    /// <param name="to">дата месяца, до конца которого требуется получить информацию о зарплатных транзакциях</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaryTransactionResponse>>> GetTransactionsByDate(DateTime? from, DateTime? to)
    {
        var transactions = await salaryTransactionsService.GetTransactionsAsync(from, to);
        return Ok(transactions);
    }

    /// <summary>
    /// Получить информацию о зарплатных транзакциях конкретного сотрудника.
    /// </summary>
    /// <param name="employeeId">id сотрудника информацию о зарплатных транзакциях по изменению зарплаты которого требуется получить</param>
    /// <param name="from">дата месяца, начиная с которого  требуется получить информацию о зарплатных транзациях</param>
    /// <param name="to">дата месяца, до конца которого требуется получить информацию о зарплатных транзакциях</param>
    /// <returns></returns>
    [HttpGet("by-employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<SalaryTransactionResponse>>> GetTransactionsByEmployeeId(Guid employeeId, DateTime? from, DateTime? to)
    {
        var transactions = await salaryTransactionsService.GetTransactionsByEmployeeIdAsync(employeeId, from, to);
        return Ok(transactions);
    }

    /// <summary>
    /// Получить информацию о зарплатных транзакциях по Id сотрудника внесшего изменение в зарплату.
    /// </summary>
    /// <param name="changedBy">id сотрудника внесшего изменение в зарплату.</param>
    /// <param name="from">дата месяца, начиная с которого  требуется получить информацию о зарплатных транзациях</param>
    /// <param name="to">дата месяца, до конца которого требуется получить информацию о зарплатных транзакциях</param>
    /// <returns></returns>
    [HttpGet("by-manager/{managerId}")]
    public async Task<ActionResult<IEnumerable<SalaryTransactionResponse>>> GetTransactionsByManagerId(Guid changedBy, DateTime? from, DateTime? to)
    {
        var transactions = await salaryTransactionsService.GetTransactionsByManagerIdAsync(changedBy, from, to);
        return Ok(transactions);
    }
}
