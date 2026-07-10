using GestionDesPresences.AI.Models;

namespace GestionDesPresences.AI.Services.Employees;

public interface IEmployeeAIService
{
    Task<EmployeeCardResult?> GetEmployeeCardByNameAsync(string? employeeName);

    Task<EmployeeCardResult?> GetEmployeeCardByIdAsync(int id);
}