using AFIRegistration.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AFIRegistration.Api.Contexts
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
