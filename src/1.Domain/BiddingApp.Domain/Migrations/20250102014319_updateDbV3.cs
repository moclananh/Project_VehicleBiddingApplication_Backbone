using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiddingApp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateDbV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desciption",
                table: "Vehicles",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "Horsepower",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MaximumSpeed",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfChairs",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TrunkCapacity",
                table: "Vehicles",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horsepower",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MaximumSpeed",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "NumberOfChairs",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TrunkCapacity",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Vehicles",
                newName: "Desciption");
        }
    }
}
