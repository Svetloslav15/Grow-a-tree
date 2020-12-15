using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class AddTreePostReplyReaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreePostReplyReactions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    TreePostReplyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreePostReplyReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreePostReplyReactions_TreePostReplies_TreePostReplyId",
                        column: x => x.TreePostReplyId,
                        principalTable: "TreePostReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TreePostReplyReactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TreePostReplyReactions_TreePostReplyId",
                table: "TreePostReplyReactions",
                column: "TreePostReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_TreePostReplyReactions_UserId",
                table: "TreePostReplyReactions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TreePostReplyReactions");
        }
    }
}
