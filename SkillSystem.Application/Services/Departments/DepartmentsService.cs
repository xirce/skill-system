using Mapster;
using SkillSystem.Application.Common.Exceptions;
using SkillSystem.Application.Repositories;
using SkillSystem.Application.Repositories.Departments;
using SkillSystem.Application.Repositories.Employees;
using SkillSystem.Application.Services.Departments.Models;
using SkillSystem.Application.Services.Employees.Models;
using SkillSystem.Core.Entities;

namespace SkillSystem.Application.Services.Departments;

public class DepartmentsService : IDepartmentsService
{
    private readonly IDepartmentsRepository departmentsRepository;
    private readonly IEmployeesRepository employeesRepository;
    private readonly IUnitOfWork unitOfWork;

    public DepartmentsService(
        IDepartmentsRepository departmentsRepository,
        IEmployeesRepository employeesRepository,
        IUnitOfWork unitOfWork)
    {
        this.departmentsRepository = departmentsRepository;
        this.employeesRepository = employeesRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<int> CreateDepartment(CreateDepartmentRequest request)
    {
        var department = await departmentsRepository.FindDepartmentByName(request.Name);

        if (department is not null)
            throw new EntityAlreadyExistsException($"Department with name '{request.Name}' already exists");

        var newDepartment = request.Adapt<Department>();
        await departmentsRepository.CreateDepartment(newDepartment);
        await unitOfWork.SaveChangesAsync();

        return newDepartment.Id;
    }

    public async Task<DepartmentResponse> GetDepartmentById(int departmentId)
    {
        var department = await departmentsRepository.GetDepartmentById(departmentId);
        return department.Adapt<DepartmentResponse>();
    }

    public async Task<GetDepartmentEmployeesResponse> GetDepartmentEmployees(int departmentId)
    {
        var departmentEmployees = await departmentsRepository.GetDepartmentEmployees(departmentId);
        var employeeShortInfos = departmentEmployees.Adapt<IReadOnlyCollection<EmployeeShortInfo>>();
        return new GetDepartmentEmployeesResponse(departmentId, employeeShortInfos);
    }

    public async Task<IReadOnlyCollection<DepartmentResponse>> GetAllDepartments()
    {
        var departments = await departmentsRepository.GetAllDepartments();
        return departments.Adapt<IReadOnlyCollection<DepartmentResponse>>();
    }

    public async Task AddEmployeeToDepartment(AddEmployeeToDepartmentRequest request)
    {
        var employee = await employeesRepository.GetEmployeeById(request.EmployeeId);
        await departmentsRepository.AddEmployeeToDepartment(request.DepartmentId, employee);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task RemoveEmployeeFromDepartment(RemoveEmployeeFromDepartmentRequest request)
    {
        await departmentsRepository.RemoveEmployeeFromDepartment(request.DepartmentId, request.EmployeeId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task SetHeadEmployee(SetHeadEmployeeRequest request)
    {
        var department = await departmentsRepository.GetDepartmentById(request.DepartmentId);
        var employee = await employeesRepository.GetEmployeeById(request.EmployeeId);

        department.HeadEmployeeId = employee.Id;

        departmentsRepository.UpdateDepartment(department);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateDepartment(UpdateDepartmentRequest request)
    {
        var department = await departmentsRepository.GetDepartmentById(request.DepartmentId);

        request.Adapt(department);

        departmentsRepository.UpdateDepartment(department);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteDepartment(int departmentId)
    {
        var department = await departmentsRepository.GetDepartmentById(departmentId);

        departmentsRepository.DeleteDepartment(department);
        await unitOfWork.SaveChangesAsync();
    }
}
