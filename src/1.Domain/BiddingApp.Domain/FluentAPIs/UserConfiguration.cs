using BiddingApp.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace BiddingApp.Domain.FluentAPIs
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd(); // Automatically generates a new GUID

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(u => u.Username)
                   .IsUnique(); // Ensures unique usernames

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(u => u.Email)
                   .IsUnique(); // Ensures unique emails

            builder.Property(u => u.Password)
                   .IsRequired();

            builder.Property(u => u.Role)
                   .IsRequired();

            builder.Property(u => u.Budget)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Specifies decimal format for budget

            // Relationships
            builder.HasMany(u => u.Bids)
                   .WithOne(b => b.User)
                   .HasForeignKey(b => b.UserId)
                   .OnDelete(DeleteBehavior.Cascade); // Deletes user's bids if the user is deleted
        }
    }
}
