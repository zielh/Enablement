using Microsoft.EntityFrameworkCore;

namespace SampleRestAPI2.DAL
{
    public class SampleRestAPI2Context : DbContext
    {
        public SampleRestAPI2Context(DbContextOptions<SampleRestAPI2Context> options) : base(options)
        { }

        public DbSet<Models.Countries> Countries { get; set; }
        public DbSet<Models.Merchants> Merchants { get; set; }
        public DbSet<Models.Orders> Orders { get; set; }
        public DbSet<Models.OrdersItems> OrdersItems { get; set; }
        public DbSet<Models.Products> Products { get; set; }
        public DbSet<Models.Users> Users { get; set; }

    }
}
