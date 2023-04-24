using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookBook.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IngUnitCreatedInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Units",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Ingridients",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Ingridients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Units_CreatedById",
                table: "Units",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ingridients_CreatedById",
                table: "Ingridients",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingridients_AspNetUsers_CreatedById",
                table: "Ingridients",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_AspNetUsers_CreatedById",
                table: "Units",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingridients_AspNetUsers_CreatedById",
                table: "Ingridients");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_AspNetUsers_CreatedById",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_CreatedById",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Ingridients_CreatedById",
                table: "Ingridients");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Ingridients");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Ingridients");
        }
    }
}
