using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Foodies.Api.Migrations
{
    public partial class AddFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51040700-0e9c-4698-87a1-667c19163f33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2620122-1d02-41a3-ae50-7f7ea0eb2985");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN1",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN2",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN3",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN4",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN5",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN6",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN7",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IngredientN8",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN1",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN2",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN3",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN4",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN5",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN6",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN7",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PreparationN8",
                table: "Recipes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "03e955ec-b3c7-4ffd-99c7-8809b1461263", "2", "User", "User" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "095f7aec-09b0-4cbe-b716-cc83093fbf22", "1", "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03e955ec-b3c7-4ffd-99c7-8809b1461263");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "095f7aec-09b0-4cbe-b716-cc83093fbf22");

            migrationBuilder.DropColumn(
                name: "IngredientN1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientN2",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientN3",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientN4",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientN5",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientN6",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientN7",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientN8",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN2",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN3",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN4",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN5",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN6",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN7",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationN8",
                table: "Recipes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "51040700-0e9c-4698-87a1-667c19163f33", "1", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2620122-1d02-41a3-ae50-7f7ea0eb2985", "2", "User", "User" });
        }
    }
}
