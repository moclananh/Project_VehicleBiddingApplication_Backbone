using BiddingApp.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BiddingApp.Domain.FluentAPIs
{
    public class BiddingConfiguration : IEntityTypeConfiguration<Bidding>
    {
        public void Configure(EntityTypeBuilder<Bidding> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.UserCurrentBidding)
                   .IsRequired();

            builder.Property(b => b.IsWinner)
                   .IsRequired();

            builder.HasOne(b => b.User)
                   .WithMany(u => u.Bids)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.BiddingSession)
                   .WithMany(bs => bs.Biddings)
                   .HasForeignKey(b => b.BiddingSessionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
