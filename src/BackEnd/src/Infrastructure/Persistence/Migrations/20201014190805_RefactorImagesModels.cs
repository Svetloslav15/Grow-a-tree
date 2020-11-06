using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class RefactorImagesModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Trees_TreeId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TreeId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Trees");

            migrationBuilder.DropColumn(
                name: "TreeId",
                table: "Images");

            migrationBuilder.CreateTable(
                name: "TreeImage",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    TreeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreeImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreeImage_Trees_TreeId",
                        column: x => x.TreeId,
                        principalTable: "Trees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreeImage_TreeId",
                table: "TreeImage",
                column: "TreeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreeImage");

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Trees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TreeId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_TreeId",
                table: "Images",
                column: "TreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Trees_TreeId",
                table: "Images",
                column: "TreeId",
                principalTable: "Trees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
