using ASP_CRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASP_CRUD.Data
{
    public class MVCDemoContext : DbContext
    {
        public MVCDemoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
