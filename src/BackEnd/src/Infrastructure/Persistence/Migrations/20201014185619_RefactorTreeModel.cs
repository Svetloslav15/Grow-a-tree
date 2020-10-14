using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrowATree.Infrastructure.Persistence.Migrations
{
    public partial class RefactorTreeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantedAt",
                table: "Trees");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "Trees",
                newName: "Nickname");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Trees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Trees",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Trees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Trees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Trees",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlantedOn",
                table: "Trees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Trees");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Trees");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Trees");

            migrationBuilder.DropColumn(
                name: "PlantedOn",
                table: "Trees");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Trees",
                newName: "NickName");

            migrationBuilder.AlterColumn<string>(
                name: "Longitude",
                table: "Trees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Latitude",
                table: "Trees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "PlantedAt",
                table: "Trees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
