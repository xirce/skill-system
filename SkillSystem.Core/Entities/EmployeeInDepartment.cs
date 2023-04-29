namespace SkillSystem.Core.Entities;

/// <summary>
/// Сотрудник в отделе.
/// </summary>
public class EmployeeInDepartment
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    public Employee Employee { get; set; }

    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Отдел, в котором находится сотрудник.
    /// </summary>
    public Department Department { get; set; }

    public int DepartmentId { get; set; }

    public EmployeeInDepartment(int departmentId, Guid employeeId)
    {
        DepartmentId = departmentId;
        EmployeeId = employeeId;
    }
}
