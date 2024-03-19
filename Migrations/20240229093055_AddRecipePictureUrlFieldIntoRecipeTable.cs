using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodies.Api.Migrations
{
    public partial class AddRecipePictureUrlFieldIntoRecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "512b8515-1c41-4bbc-b570-cb831be77f79");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f15136fb-0bfd-44fd-934e-3879b3957975");

            migrationBuilder.DropColumn(
                name: "RecipePicture",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "Recipes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Recipes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cacfa70-375c-499a-93b8-2da910329e6b", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fc80c93-6ba3-40a9-a95f-3d641c36dcba", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cacfa70-375c-499a-93b8-2da910329e6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fc80c93-6ba3-40a9-a95f-3d641c36dcba");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Recipes");

            migrationBuilder.AddColumn<byte[]>(
                name: "RecipePicture",
                table: "Recipes",
                type: "longblob",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "512b8515-1c41-4bbc-b570-cb831be77f79", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f15136fb-0bfd-44fd-934e-3879b3957975", "2", "User", "User" });
        }
    }
}
