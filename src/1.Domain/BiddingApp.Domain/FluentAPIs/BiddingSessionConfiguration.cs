using BiddingApp.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiddingApp.Domain.FluentAPIs
{
    public class BiddingSessionConfiguration : IEntityTypeConfiguration<BiddingSession>
    {
        public void Configure(EntityTypeBuilder<BiddingSession> builder)
        {
            builder.HasKey(bs => bs.Id);

            builder.Property(bs => bs.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(bs => bs.StartTime)
                   .IsRequired();

            builder.Property(bs => bs.EndTime)
                   .IsRequired();

            builder.Property(bs => bs.TotalBiddingCount)
                   .IsRequired();

            builder.Property(bs => bs.HighestBidding)
                   .IsRequired();

            builder.Property(bs => bs.IsClosed)
                   .IsRequired();

            builder.HasOne(bs => bs.Vehicle)
                   .WithMany(v => v.BiddingSessions)
                   .HasForeignKey(bs => bs.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(bs => bs.Bids)
                   .WithOne(b => b.BiddingSession)
                   .HasForeignKey(b => b.BiddingSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
