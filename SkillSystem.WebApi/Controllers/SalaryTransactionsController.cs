using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaryTransactionResponse>>> GetTransactionsByDate(DateTime? from, DateTime? to)
    {
        var transactions = await salaryTransactionsService.GetTransactionsAsync(from, to);
        return Ok(transactions);
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<SalaryTransactionResponse>>> GetTransactionsByEmployeeId([BindRequired] Guid employeeId, DateTime? from, DateTime? to)
    {
        var transactions = await salaryTransactionsService.GetTransactionsByEmployeeIdAsync(employeeId, from, to);
        return Ok(transactions);
    }

    [HttpGet("by-manager/{managerId}")]
    public async Task<ActionResult<IEnumerable<SalaryTransactionResponse>>> GetTransactionsByManagerId([BindRequired] Guid managerId, DateTime? from, DateTime? to)
    {
        var transactions = await salaryTransactionsService.GetTransactionsByManagerIdAsync(managerId, from, to);
        return Ok(transactions);
    }
}
