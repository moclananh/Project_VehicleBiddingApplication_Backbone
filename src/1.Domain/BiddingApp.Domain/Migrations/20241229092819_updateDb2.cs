using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiddingApp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateDb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBiddingCount",
                table: "Biddings");

            migrationBuilder.RenameColumn(
                name: "StartingPrice",
                table: "Vehicles",
                newName: "Price");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "UserBiddingCount",
               table: "Biddings");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Vehicles",
                newName: "StartingPrice");

        }
    }
}
