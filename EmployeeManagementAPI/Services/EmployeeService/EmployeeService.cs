
using EmployeeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;
        public EmployeeService(DataContext context) 
        { 
            _context = context;
        }
        public async Task<List<Employee>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>?> DeleteEmployeebyId(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return null;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return await _context.Employees.ToListAsync();
            
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee?> GetEmployeebyId(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return null;
            return employee;
        }

        public async Task<List<Employee>?> UpdateEmployee(int id, Employee request)
        {
            var employee = await _context.Employees.FindAsync(id);
            
            if (employee == null)
                return null;

            employee.Name = request.Name;
            employee.Role = request.Role;
            employee.Company = request.Company;
            //employee.UserName = request.UserName;
            //employee.PasswordHash = request.PasswordHash;

            await _context.SaveChangesAsync();

            return await _context.Employees.ToListAsync();
        }
        //public async Task<Company>? GetCompanyById(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //        return null;
        //    if(employee.CompanyId == id)
        //        Company = await _context.Companys.FindAsync(id);
        //
        //}
    }
}
