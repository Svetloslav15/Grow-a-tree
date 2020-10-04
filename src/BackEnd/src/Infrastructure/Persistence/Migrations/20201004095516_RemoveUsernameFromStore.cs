using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class RemoveUsernameFromStore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Stores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
