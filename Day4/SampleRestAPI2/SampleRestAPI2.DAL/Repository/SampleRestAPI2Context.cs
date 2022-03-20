using Microsoft.EntityFrameworkCore;
using SampleRestAPI2.DAL.Models;

namespace SampleRestAPI2.DAL.Repository
{
    public class SampleRestAPI2Context : DbContext
    {
        public SampleRestAPI2Context(DbContextOptions<SampleRestAPI2Context> options) : base(options)
        { }

        public DbSet<Countries> Countries { get; set; }
        public DbSet<Merchants> Merchants { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrdersItems> OrdersItems { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Logs> Logs { get; set; }

    }
}
