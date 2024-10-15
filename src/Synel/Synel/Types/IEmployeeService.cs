using Synel.Models;

namespace Synel.Types
{
    public interface IEmployeeService
    {
        Task<int> InsertFileDataAsync(IFormFile file);
        Task<List<Employees>> GetEmployeesAsync(string? name = null, SortOrder? sortOrder = SortOrder.ACS, SortFields? sortField = SortFields.Surname);
        Task<bool> UpdateEmployeeAsync(Employees employee);
        Task<Employees> GetEmployeeByIdAsync(int id);
    }
}
