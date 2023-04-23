using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AuthorAndDateAddedToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Recipes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorId",
                table: "Recipes",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AuthorId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AuthorId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Recipes");
        }
    }
}
