using Acxiom73.Models;
using Microsoft.EntityFrameworkCore;

namespace Acxiom73.Data
{
    public class Dbcontext:DbContext
    {
        public Dbcontext(DbContextOptions<Dbcontext> options) : base(options)
        {

        }
        public DbSet<CRMUser>CRMUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
