using Microsoft.EntityFrameworkCore;
using Moamen.Training.APIs.Models;

namespace Moamen.Training.APIs.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
