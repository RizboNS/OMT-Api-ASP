using Microsoft.EntityFrameworkCore;
using OMT_Api.Entities;

namespace OMT_Api.Data
{
    public class OmtDbContext : DbContext
    {
        public OmtDbContext(DbContextOptions<OmtDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}