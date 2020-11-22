using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class RefactorReportModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "TreeReports");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TreeReports",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TreeReports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpam",
                table: "TreeReports",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "TreeReports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "TreeReports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TreeReports");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TreeReports");

            migrationBuilder.DropColumn(
                name: "IsSpam",
                table: "TreeReports");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "TreeReports");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "TreeReports");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "TreeReports",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
