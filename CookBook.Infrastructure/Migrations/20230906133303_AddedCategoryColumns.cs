using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCategoryColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "RecipeCategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "RecipeCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 33, 3, 822, DateTimeKind.Local).AddTicks(6792));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastEdit",
                table: "RecipeCategories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeCategories_CreatedById",
                table: "RecipeCategories",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeCategories_AspNetUsers_CreatedById",
                table: "RecipeCategories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeCategories_AspNetUsers_CreatedById",
                table: "RecipeCategories");

            migrationBuilder.DropIndex(
                name: "IX_RecipeCategories_CreatedById",
                table: "RecipeCategories");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "RecipeCategories");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "RecipeCategories");

            migrationBuilder.DropColumn(
                name: "LastEdit",
                table: "RecipeCategories");
        }
    }
}
