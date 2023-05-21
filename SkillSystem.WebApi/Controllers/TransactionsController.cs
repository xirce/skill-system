using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SkillSystem.Application.Services.Transactions;
using SkillSystem.Application.Services.Transactions.Models;

namespace SkillSystem.WebApi.Controllers;

[Route("api/transactions")]
public class TransactionsController : BaseController
{
    private readonly ITransactionsService transactionsService;

    public TransactionsController(ITransactionsService transactionsService)
    {
        this.transactionsService = transactionsService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetTransactionsByDate(DateTime? from, DateTime? to)
    {
        var transactions = await transactionsService.GetTransactionsAsync(from, to);
        return Ok(transactions);
    }

    [HttpGet("by-employee/{employeeId}")]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetTransactionsByEmployeeId([BindRequired] Guid employeeId, DateTime? from, DateTime? to)
    {
        var transactions = await transactionsService.GetTransactionsByEmployeeIdAsync(employeeId, from, to);
        return Ok(transactions);
    }

    [HttpGet("by-manager/{managerId}")]
    public async Task<ActionResult<IEnumerable<TransactionResponse>>> GetTransactionsByManagerId([BindRequired] Guid managerId, DateTime? from, DateTime? to)
    {
        var transactions = await transactionsService.GetTransactionsByManagerIdAsync(managerId, from, to);
        return Ok(transactions);
    }
}
