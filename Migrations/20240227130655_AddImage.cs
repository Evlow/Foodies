using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodies.Api.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03e955ec-b3c7-4ffd-99c7-8809b1461263");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "095f7aec-09b0-4cbe-b716-cc83093fbf22");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03e955ec-b3c7-4ffd-99c7-8809b1461263", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "095f7aec-09b0-4cbe-b716-cc83093fbf22", "1", "Admin", "Admin" });
        }
    }
}
