using EmployeesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeesAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
