using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodies.Api.Migrations
{
    public partial class AddRecipePictureUrlNullFieldIntoRecipeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cacfa70-375c-499a-93b8-2da910329e6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fc80c93-6ba3-40a9-a95f-3d641c36dcba");

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "Recipes",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ba7eef3b-b2b9-44ce-95ec-b56c02170365", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "eedab828-c292-4b36-8eab-2822791bfcf4", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba7eef3b-b2b9-44ce-95ec-b56c02170365");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eedab828-c292-4b36-8eab-2822791bfcf4");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "PublicId",
                keyValue: null,
                column: "PublicId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PublicId",
                table: "Recipes",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cacfa70-375c-499a-93b8-2da910329e6b", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fc80c93-6ba3-40a9-a95f-3d641c36dcba", "1", "Admin", "Admin" });
        }
    }
}
