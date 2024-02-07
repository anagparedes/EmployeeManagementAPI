
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Services
{
    public class EmployeeService: IEmployeeService
    {
        public bool Validate(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                return false;
            }

            return true;
        }
    }
}
