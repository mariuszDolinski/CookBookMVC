using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SettingFKConstrOnIngCategoryId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingridients_IngridientCategories_IngridientCategoryCategoryId",
                table: "Ingridients");

            migrationBuilder.DropIndex(
                name: "IX_Ingridients_IngridientCategoryCategoryId",
                table: "Ingridients");

            migrationBuilder.DropColumn(
                name: "IngridientCategoryCategoryId",
                table: "Ingridients");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Ingridients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_CategoryId",
                table: "Ingridients",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingridients_IngridientCategories_CategoryId",
                table: "Ingridients",
                column: "CategoryId",
                principalTable: "IngridientCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingridients_IngridientCategories_CategoryId",
                table: "Ingridients");

            migrationBuilder.DropIndex(
                name: "IX_Ingridients_CategoryId",
                table: "Ingridients");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Ingridients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IngridientCategoryCategoryId",
                table: "Ingridients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_IngridientCategoryCategoryId",
                table: "Ingridients",
                column: "IngridientCategoryCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingridients_IngridientCategories_IngridientCategoryCategoryId",
                table: "Ingridients",
                column: "IngridientCategoryCategoryId",
                principalTable: "IngridientCategories",
                principalColumn: "CategoryId");
        }
    }
}
