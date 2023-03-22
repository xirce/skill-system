using Microsoft.AspNetCore.Mvc;
using SkillSystem.Application.Services.Salaries;
using SkillSystem.Application.Services.Salaries.Models;

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
    public async Task<ActionResult<int>> CreateSalary(SalaryRequest request)
    {
        var salaryDate = DateTime.Now.Month == 12? new DateTime(DateTime.Now.Year + 1, 1, DateTime.Now.Day, 0, 0, 0, DateTimeKind.Utc) :
            new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, DateTime.Now.Day, 0, 0, 0, DateTimeKind.Utc);
        var salaryId = await salariesService.CreateSalaryAsync(request, salaryDate);
        return Ok(salaryId);
    }

    /// <summary>
    /// Получить информацию о зарплатах сотрудника.
    /// </summary>
    /// <param name="employeeId">id сотрудника, информацию о зарплате которого требуется получить</param>
    /// <param name="from">дата, начиная с которой требуется получить информацию о зарплате</param>
    /// <param name="to">дата, заканчивая которой требуется получить информацию о зарплате</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaryResponse>>> GetSalaries(Guid? employeeId = null, DateTime? from = null, DateTime? to = null)
    {
        var salaries = await salariesService.GetSalariesAsync(employeeId, from, to);
        return Ok(salaries);
    }

    /// <summary>
    /// Получить информацию о конкретной зарплате.
    /// </summary>
    /// <param name="salaryId">id зарплаты</param>
    /// <returns></returns>
    [HttpGet("{salaryId}")]
    public async Task<ActionResult<SalaryResponse>> GetSalaryById(int salaryId)
    {
        var salary = await salariesService.GetSalaryByIdAsync(salaryId);
        return Ok(salary);
    }


    /// <summary>
    /// Изменить информацию о конкретной зарплате.
    /// </summary>
    /// <param name="salaryId">id зарплаты</param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{salaryId}")]
    public async Task<IActionResult> UpdateSalary(int salaryId, SalaryRequest request)
    {
        var salary = await salariesService.GetSalaryByIdAsync(salaryId);
        if (salary.SalaryDate.Month > DateTime.Now.Month)
            await salariesService.UpdateSalaryAsync(salaryId, request);
        return NoContent();
    }

    /// <summary>
    /// Удалить информацию о конкретной зарплате.
    /// </summary>
    /// <param name="salaryId">id зарплаты</param>
    /// <returns></returns>
    [HttpDelete("{salaryId}")]
    public async Task<IActionResult> DeleteSalary(int salaryId)
    {
        var salary = await salariesService.GetSalaryByIdAsync(salaryId);
        if (salary.SalaryDate.Month > DateTime.Now.Month)
            await salariesService.DeleteSalaryAsync(salaryId);
        return NoContent();
    }
}
