using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class RefferalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferrerId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReffererId",
                table: "AspNetUsers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ReffererId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReffererId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReferrerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReffererId",
                table: "AspNetUsers");
        }
    }
}
