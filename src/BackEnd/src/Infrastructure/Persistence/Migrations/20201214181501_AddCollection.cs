using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class AddCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreePostReply_TreePosts_TreePostId",
                table: "TreePostReply");

            migrationBuilder.DropForeignKey(
                name: "FK_TreePostReply_AspNetUsers_UserId",
                table: "TreePostReply");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreePostReply",
                table: "TreePostReply");

            migrationBuilder.RenameTable(
                name: "TreePostReply",
                newName: "TreePostReplies");

            migrationBuilder.RenameIndex(
                name: "IX_TreePostReply_UserId",
                table: "TreePostReplies",
                newName: "IX_TreePostReplies_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TreePostReply_TreePostId",
                table: "TreePostReplies",
                newName: "IX_TreePostReplies_TreePostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreePostReplies",
                table: "TreePostReplies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreePostReplies_TreePosts_TreePostId",
                table: "TreePostReplies",
                column: "TreePostId",
                principalTable: "TreePosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreePostReplies_AspNetUsers_UserId",
                table: "TreePostReplies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TreePostReplies_TreePosts_TreePostId",
                table: "TreePostReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_TreePostReplies_AspNetUsers_UserId",
                table: "TreePostReplies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreePostReplies",
                table: "TreePostReplies");

            migrationBuilder.RenameTable(
                name: "TreePostReplies",
                newName: "TreePostReply");

            migrationBuilder.RenameIndex(
                name: "IX_TreePostReplies_UserId",
                table: "TreePostReply",
                newName: "IX_TreePostReply_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TreePostReplies_TreePostId",
                table: "TreePostReply",
                newName: "IX_TreePostReply_TreePostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreePostReply",
                table: "TreePostReply",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TreePostReply_TreePosts_TreePostId",
                table: "TreePostReply",
                column: "TreePostId",
                principalTable: "TreePosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreePostReply_AspNetUsers_UserId",
                table: "TreePostReply",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
