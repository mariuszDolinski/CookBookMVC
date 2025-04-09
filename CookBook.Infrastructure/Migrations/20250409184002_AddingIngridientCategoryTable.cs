using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIngridientCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngridientCategoryCategoryId",
                table: "Ingridients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IngridientCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2025, 4, 9, 20, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastEdit = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngridientCategories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_IngridientCategories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_IngridientCategoryCategoryId",
                table: "Ingridients",
                column: "IngridientCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IngridientCategories_CreatedById",
                table: "IngridientCategories",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingridients_IngridientCategories_IngridientCategoryCategoryId",
                table: "Ingridients",
                column: "IngridientCategoryCategoryId",
                principalTable: "IngridientCategories",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingridients_IngridientCategories_IngridientCategoryCategoryId",
                table: "Ingridients");

            migrationBuilder.DropTable(
                name: "IngridientCategories");

            migrationBuilder.DropIndex(
                name: "IX_Ingridients_IngridientCategoryCategoryId",
                table: "Ingridients");

            migrationBuilder.DropColumn(
                name: "IngridientCategoryCategoryId",
                table: "Ingridients");
        }
    }
}
