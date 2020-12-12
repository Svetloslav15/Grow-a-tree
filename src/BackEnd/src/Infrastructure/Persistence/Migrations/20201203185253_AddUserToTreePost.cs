using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class AddUserToTreePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TreePosts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TreePosts_UserId",
                table: "TreePosts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TreePosts_AspNetUsers_UserId",
                table: "TreePosts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreePosts_AspNetUsers_UserId",
                table: "TreePosts");

            migrationBuilder.DropIndex(
                name: "IX_TreePosts_UserId",
                table: "TreePosts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TreePosts");
        }
    }
}
