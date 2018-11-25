using Microsoft.EntityFrameworkCore.Migrations;

namespace Orlen.Core.Migrations
{
    public partial class PointRoute2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutePoint");

            migrationBuilder.CreateTable(
                name: "RoutePoints",
                columns: table => new
                {
                    RouteId = table.Column<int>(nullable: false),
                    PointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePoints", x => new { x.RouteId, x.PointId });
                    table.ForeignKey(
                        name: "FK_RoutePoints_Points_PointId",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutePoints_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_PointId",
                table: "RoutePoints",
                column: "PointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutePoints");

            migrationBuilder.CreateTable(
                name: "RoutePoint",
                columns: table => new
                {
                    RouteId = table.Column<int>(nullable: false),
                    PointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePoint", x => new { x.RouteId, x.PointId });
                    table.ForeignKey(
                        name: "FK_RoutePoint_Points_PointId",
                        column: x => x.PointId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutePoint_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoint_PointId",
                table: "RoutePoint",
                column: "PointId");
        }
    }
}
