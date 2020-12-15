using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class SpellingMistakes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ReffererId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReffererId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReffererId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "RefererId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RefererId",
                table: "AspNetUsers",
                column: "RefererId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_RefererId",
                table: "AspNetUsers",
                column: "RefererId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_RefererId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RefererId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefererId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ReffererId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReffererId",
                table: "AspNetUsers",
                column: "ReffererId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ReffererId",
                table: "AspNetUsers",
                column: "ReffererId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
