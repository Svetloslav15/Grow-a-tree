using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class GuessGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnknownTrees",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    TreeName = table.Column<string>(nullable: true),
                    ClosestResults = table.Column<string>(nullable: true),
                    Votes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownTrees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnknownTrees");
        }
    }
}
