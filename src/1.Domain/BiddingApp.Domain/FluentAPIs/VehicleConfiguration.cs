using BiddingApp.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BiddingApp.Domain.FluentAPIs
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(v => v.Brands)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(v => v.VIN)
                   .IsUnique();
           
            builder.Property(v => v.Price)
                   .IsRequired();

            builder.Property(v => v.Color)
                   .HasMaxLength(30);

            builder.Property(v => v.ImageUrl)
                   .HasMaxLength(255);

            builder.Property(v => v.Status)
                   .IsRequired();

            builder.Property(v => v.NumberOfChairs)
                   .IsRequired(); 

            builder.Property(v => v.Horsepower)
                   .IsRequired();

            builder.Property(v => v.MaximumSpeed)
                   .IsRequired();

            builder.Property(v => v.TrunkCapacity)
                   .IsRequired()
                   .HasPrecision(10, 2);

            builder.HasMany(v => v.BiddingSessions)
                   .WithOne(bs => bs.Vehicle)
                   .HasForeignKey(bs => bs.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
