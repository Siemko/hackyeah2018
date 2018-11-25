using Microsoft.EntityFrameworkCore.Migrations;

namespace Orlen.Core.Migrations
{
    public partial class FieldsRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lat",
                newName: "Latitude",
                table: "Points");

            migrationBuilder.RenameColumn(
                name: "Lon",
                newName: "Longitude",
                table: "Points");
        }
    }
}
