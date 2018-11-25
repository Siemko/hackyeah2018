using Microsoft.EntityFrameworkCore.Migrations;

namespace Orlen.Core.Migrations
{
    public partial class IssueTypeValuePrecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Issues",
                type: "decimal(18, 6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "Issues",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)",
                oldNullable: true);
        }
    }
}
