using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeManagementAPI.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

    }
}
