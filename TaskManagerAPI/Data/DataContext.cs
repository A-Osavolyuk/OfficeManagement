using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models.Entities;

namespace TaskManagerAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<OfficeTask> Tasks { get; set; }
        public DbSet<EmployeeTask> EmployeesTasks { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
