using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SkillSystem.Application.Services.Salaries;
using SkillSystem.Application.Services.Transactions;
using SkillSystem.Application.Services.Salaries.Models;
using System.ComponentModel.DataAnnotations;

namespace SkillSystem.WebApi.Controllers;

[Route("api/salaries")]
public class SalariesController : BaseController
{
    private readonly ISalariesService salariesService;

    public SalariesController(ISalariesService salariesService)
    {
        this.salariesService = salariesService;
    }

    /// <summary>
    /// Назначить сотруднику зарплату.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<int>> SaveSalary([FromServices] ITransactionsService transactionsService, SalaryRequest request, Guid managerId)
    {
        try
        {
            var salary = await salariesService.SaveSalaryAsync(request);
            var salaryId = await transactionsService.SaveTransactionAsync(salary, managerId);
            return Ok(salaryId);
        } catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Получить информацию о конкретной зарплате.
    /// </summary>
    /// <param name="salaryId">id операции назначения зарплаты</param>
    /// <returns></returns>
    [HttpGet("{salaryId}")]
    public async Task<ActionResult<SalaryResponse>> GetSalaryById(int salaryId)
    {
        var salary = await salariesService.GetSalaryByIdAsync(salaryId);
        return Ok(salary);
    }

    /// <summary>
    /// Получить информацию о зарплате сотрудника в определенном месяце.
    /// </summary>
    /// <param name="date"> дата месяца, информацию о зарплате в котором, требуется получить </param>
    /// <param name="employeeId"> id сотрудника информацию о зарплате которого требуется получить </param>
    /// <returns></returns>
    [HttpGet("by-month/{date}")]
    public async Task<ActionResult<SalaryResponse>> GetSalaryByMonth(DateTime date, [BindRequired] Guid employeeId)
    {
        var salary = await salariesService.GetSalaryByMonthAsync(employeeId, date);
        return Ok(salary);
    }


    /// <summary>
    /// Получить информацию о текущей зарплате сотрудника.
    /// </summary>
    /// <param name="employeeId"> id сотрудника информацию о зарплате которого требуется получить </param>
    /// <returns></returns>
    [HttpGet("current")]
    public async Task<ActionResult<SalaryResponse>> GetCurrentSalary(Guid employeeId)
    {
        var salary = await salariesService.GetCurrentSalaryAsync(employeeId);
        return Ok(salary);
    }

    /// <summary>
    /// Получить информацию о зарплатах сотрудника.
    /// </summary>
    /// <param name="employeeId">id сотрудника информацию о зарплате которого требуется получить</param>
    /// <param name="from">дата месяца, начиная с которого  требуется получить информацию о зарплатах сотрудника</param>
    /// <param name="to">дата месяца, до конца которого требуется получить информацию о зарплатах сотрудника</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaryResponse>>> GetSalaries([BindRequired] Guid employeeId, DateTime? from, DateTime? to)
    {
        var salaries = await salariesService.GetSalariesAsync(employeeId, from, to);
        return Ok(salaries);
    }

    /// <summary>
    /// Оменить будущее назначение зарплаты.
    /// </summary>
    /// <param name="salaryId">id операции назначения зарплаты</param>
    /// <returns></returns>
    [HttpDelete("{salaryId}")]
    public async Task<IActionResult> CancelSalaryAssigment(int salaryId)
    {
        await salariesService.CancelSalaryAssigmentAsync(salaryId);
        return NoContent();
    }
}
