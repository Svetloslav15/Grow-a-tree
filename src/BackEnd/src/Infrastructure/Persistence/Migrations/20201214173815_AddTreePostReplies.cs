using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class AddTreePostReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreePostReply",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Conent = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    TreePostId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreePostReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreePostReply_TreePosts_TreePostId",
                        column: x => x.TreePostId,
                        principalTable: "TreePosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreePostReply_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreePostReply_TreePostId",
                table: "TreePostReply",
                column: "TreePostId");

            migrationBuilder.CreateIndex(
                name: "IX_TreePostReply_UserId",
                table: "TreePostReply",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreePostReply");
        }
    }
}
