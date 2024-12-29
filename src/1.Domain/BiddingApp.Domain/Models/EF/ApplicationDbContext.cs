using BiddingApp.Domain.FluentAPIs;
using BiddingApp.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiddingApp.Domain.Models.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Bidding> Biddings { get; set; }
        public DbSet<BiddingSession> BiddingSessions { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BiddingConfiguration());
            modelBuilder.ApplyConfiguration(new BiddingSessionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
        }
    }
}
