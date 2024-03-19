using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodies.Api.Migrations
{
    public partial class AddRecipePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cb25001-d2cc-4d24-a936-98d383c40549");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43f84fda-391b-4dcd-9351-b4913a16d797");

            migrationBuilder.DropColumn(
                name: "RecipePictureData",
                table: "Recipes");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RecipePicture",
                table: "Recipes",
                type: "longblob",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "512b8515-1c41-4bbc-b570-cb831be77f79", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f15136fb-0bfd-44fd-934e-3879b3957975", "2", "User", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "512b8515-1c41-4bbc-b570-cb831be77f79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f15136fb-0bfd-44fd-934e-3879b3957975");

            migrationBuilder.AlterColumn<string>(
                name: "RecipePicture",
                table: "Recipes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "longblob",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<byte[]>(
                name: "RecipePictureData",
                table: "Recipes",
                type: "longblob",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0cb25001-d2cc-4d24-a936-98d383c40549", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "43f84fda-391b-4dcd-9351-b4913a16d797", "2", "User", "User" });
        }
    }
}
