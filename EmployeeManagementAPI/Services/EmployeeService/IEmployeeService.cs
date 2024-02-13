namespace EmployeeManagementAPI.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee?> GetEmployeebyId(int id);
        Task<List<Employee>> AddEmployee(Employee employee);
        Task<List<Employee>?> UpdateEmployee(int id, Employee employee);
        Task<List<Employee>?> DeleteEmployeebyId(int id);
    }
}
