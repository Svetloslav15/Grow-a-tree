using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class AddImagesToDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_TreeImage_Trees_TreeId",
                table: "TreeImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreeImage",
                table: "TreeImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "TreeImage",
                newName: "TreeImages");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "ProductImages");

            migrationBuilder.RenameIndex(
                name: "IX_TreeImage_TreeId",
                table: "TreeImages",
                newName: "IX_TreeImages_TreeId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_ProductId",
                table: "ProductImages",
                newName: "IX_ProductImages_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreeImages",
                table: "TreeImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreeImages_Trees_TreeId",
                table: "TreeImages",
                column: "TreeId",
                principalTable: "Trees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_TreeImages_Trees_TreeId",
                table: "TreeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TreeImages",
                table: "TreeImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.RenameTable(
                name: "TreeImages",
                newName: "TreeImage");

            migrationBuilder.RenameTable(
                name: "ProductImages",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_TreeImages_TreeId",
                table: "TreeImage",
                newName: "IX_TreeImage_TreeId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImages_ProductId",
                table: "Images",
                newName: "IX_Images_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TreeImage",
                table: "TreeImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Products_ProductId",
                table: "Images",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TreeImage_Trees_TreeId",
                table: "TreeImage",
                column: "TreeId",
                principalTable: "Trees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
