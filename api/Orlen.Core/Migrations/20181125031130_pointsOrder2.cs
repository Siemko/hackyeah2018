using Microsoft.EntityFrameworkCore.Migrations;

namespace Orlen.Core.Migrations
{
    public partial class pointsOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Points");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "RoutePoints",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "RoutePoints");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Points",
                nullable: false,
                defaultValue: 0);
        }
    }
}
