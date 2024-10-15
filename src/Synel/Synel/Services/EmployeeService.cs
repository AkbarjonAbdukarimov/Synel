using Microsoft.EntityFrameworkCore;
using Synel.Data;
using Synel.Models;
using Synel.Types;

namespace Synel.Services
{
    public class EmployeeService : IEmployeeService

    {
        // Getting context from the constructor using dependency injection
        private readonly SynelContext _context;
        IFileService fileService;
        public EmployeeService(SynelContext context, IFileService fileService)
        {
            _context = context;
            this.fileService = fileService;

        }

        public EmployeeService(FileService fileService)
        {
            this.fileService = fileService;
        }
        public EmployeeService(SynelContext context)
        {
            _context = context;

        }
        public EmployeeService()
        {
            
        }
        // Inserting data into the database from csv file and returing the number of rows inserted
        public async Task<int> InsertFileDataAsync(IFormFile file)
        {
            var emps = fileService.ReadCSVFile<Employees>(file);
            _context.Employees.AddRange(emps);
            int rowsInserted = await _context.SaveChangesAsync();
            return rowsInserted;

        }
        //retrieving data from the database and sorting by name in ascending order
        public async Task<List<Employees>> GetEmployeesAsync(string? name=null, SortOrder? sortOrder = SortOrder.ACS, SortFields? sortField = SortFields.Surname)
        {
            IQueryable<Employees> query = _context.Employees;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Fornames.Contains(name));
            }

            switch (sortField)
            {
                case SortFields.PayrolNumber:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.PayrolNumber) : query.OrderBy(e => e.PayrolNumber);
                    break;
                case SortFields.Fornames:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.Fornames) : query.OrderBy(e => e.Fornames);
                    break;
                case SortFields.Surname:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.Surname) : query.OrderBy(e => e.Surname);
                    break;
                case SortFields.DoB:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.DoB) : query.OrderBy(e => e.DoB);
                    break;
                case SortFields.Telephone:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.Telephone) : query.OrderBy(e => e.Telephone);
                    break;
                case SortFields.Mobile:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.Mobile) : query.OrderBy(e => e.Mobile);
                    break;
                case SortFields.Address:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.Address) : query.OrderBy(e => e.Address);
                    break;
                case SortFields.Address2:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.Address2) : query.OrderBy(e => e.Address2);
                    break;
                case SortFields.Postcode:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.Postcode) : query.OrderBy(e => e.Postcode);
                    break;
                case SortFields.EmailHome:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.EmailHome) : query.OrderBy(e => e.EmailHome);
                    break;
                case SortFields.StartDate:
                    query = sortOrder == SortOrder.DESC ? query.OrderByDescending(e => e.StartDate) : query.OrderBy(e => e.StartDate);
                    break;
                default:
                    query = query.OrderBy(e => e.Surname);
                    break;
            }

            return await query.ToListAsync();
        }


        public async Task<bool> UpdateEmployeeAsync(Employees employee)
        {
            var existingEmployee =await GetEmployeeByIdAsync(employee.Id);
            if (existingEmployee == null)
            {
                return false;
            }

            existingEmployee.PayrolNumber = employee.PayrolNumber;
            existingEmployee.Fornames = employee.Fornames;
            existingEmployee.Surname = employee.Surname;
            existingEmployee.DoB = employee.DoB;
            existingEmployee.Telephone = employee.Telephone;
            existingEmployee.Mobile = employee.Mobile;
            existingEmployee.Address = employee.Address;
            existingEmployee.Address2 = employee.Address2;
            existingEmployee.Postcode = employee.Postcode;
            existingEmployee.EmailHome = employee.EmailHome;
            existingEmployee.StartDate = employee.StartDate;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Employees> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

    }
}
