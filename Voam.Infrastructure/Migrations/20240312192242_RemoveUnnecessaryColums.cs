using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voam.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnnecessaryColums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "ShoppingCarts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "ShoppingCarts",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
