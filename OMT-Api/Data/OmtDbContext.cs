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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    FirstName = "Berry",
                    LastName = "Allan",
                    Email = "ballan@company.com",
                    Phone = "+381601400222",
                    Address = "Flash Street 1",
                    City = "Gotham",
                    State = "DC",
                    Zip = "10034"
                }

                );
            base.OnModelCreating(modelBuilder);
        }
    }
}