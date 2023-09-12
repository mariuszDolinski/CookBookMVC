using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CategoryNameColumnChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "RecipeCategories",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "RecipeCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 9, 6, 15, 33, 3, 822, DateTimeKind.Local).AddTicks(6792));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "RecipeCategories",
                newName: "CategoryName");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "RecipeCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 6, 15, 33, 3, 822, DateTimeKind.Local).AddTicks(6792),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
