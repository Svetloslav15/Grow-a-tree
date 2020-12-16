using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class FixSpellingMistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conent",
                table: "TreePostReply");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "TreePostReply",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "TreePostReply");

            migrationBuilder.AddColumn<string>(
                name: "Conent",
                table: "TreePostReply",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
