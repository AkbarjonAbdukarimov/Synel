using Microsoft.AspNetCore.Http;
using Synel.Models;
using Synel.Services;
using Synel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynelTests
{
    public class MockEmployeeService : IEmployeeService
    {
        public MockEmployeeService()
        {

        }
        public Task<Employees> GetEmployeeByIdAsync(int id)
        {
            var e = new Employees();
            e.Id= id;
            return Task.FromResult(e);
        }

        public Task<List<Employees>> GetEmployeesAsync( string? name = null, SortOrder? sortOrder = SortOrder.ACS, SortFields? sortField = SortFields.Surname)
        {
            List<Employees> list = new();
            return Task.FromResult(list);
        }

     

        public Task<int> InsertFileDataAsync(IFormFile file)
        {
            
            return Task.FromResult(2);
        }

        public Task<bool> UpdateEmployeeAsync(Employees employee)
        {
            return Task.FromResult(true);
        }
    }
}
