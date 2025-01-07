using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiddingApp.Domain.Migrations
{
    /// <inheritdoc />
    public partial class updateBiddingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BiddingAt",
                table: "Biddings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BiddingAt",
                table: "Biddings");
        }
    }
}
