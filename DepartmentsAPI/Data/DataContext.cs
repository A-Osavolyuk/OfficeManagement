using DepartmentsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartmentsAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
