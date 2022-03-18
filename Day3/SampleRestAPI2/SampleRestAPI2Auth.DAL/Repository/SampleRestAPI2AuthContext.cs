using Microsoft.EntityFrameworkCore;
using SampleRestAPI2Auth.DAL.Models;

namespace SampleRestAPI2Auth.DAL.Repository
{
    public class SampleRestAPI2AuthContext : DbContext
    {
        public SampleRestAPI2AuthContext(DbContextOptions<SampleRestAPI2AuthContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //add index here
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}