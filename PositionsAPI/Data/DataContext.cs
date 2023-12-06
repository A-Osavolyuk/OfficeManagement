using PositionsAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PositionsAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Position> Positions { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
