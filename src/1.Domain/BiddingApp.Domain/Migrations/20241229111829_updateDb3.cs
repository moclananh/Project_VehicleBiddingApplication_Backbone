using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiddingApp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateDb3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MinimumJumpingValue",
                table: "BiddingSessions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumJumpingValue",
                table: "BiddingSessions");
        }
    }
}
