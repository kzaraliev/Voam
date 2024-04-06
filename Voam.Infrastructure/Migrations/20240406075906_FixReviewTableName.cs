using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voam.Server.Migrations
{
    /// <inheritdoc />
    public partial class FixReviewTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_AspNetUsers_CustomerId",
                table: "ProductReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews");

            migrationBuilder.RenameTable(
                name: "ProductReviews",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReviews_ProductId",
                table: "Reviews",
                newName: "IX_Reviews_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductReviews_CustomerId",
                table: "Reviews",
                newName: "IX_Reviews_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "655a62ed-f7f0-4b80-b2e0-cf72cd152031", "AQAAAAIAAYagAAAAELbVd3c35nPxQNITFc5XClZcUFuBAZ2zVfCffzgOyAAzNWHBAHjHktcvq7AH5qkkGA==", "f7645711-2141-4f7e-b3df-ab385855e8dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2dad482e-e4eb-4511-a13a-d007110de30f", "AQAAAAIAAYagAAAAEOr/xuB/WHh/jN8GAd2vz/hJgo+1OYREH3QmPNdGogs+XPTtt82KIJCWuzXoDEk2dg==", "9797dfa5-9b3d-4513-975c-22dd0c73dd89" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_CustomerId",
                table: "Reviews",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_CustomerId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_ProductId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "ProductReviews");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ProductId",
                table: "ProductReviews",
                newName: "IX_ProductReviews_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CustomerId",
                table: "ProductReviews",
                newName: "IX_ProductReviews_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductReviews",
                table: "ProductReviews",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c3cf8d1-730c-462a-a4ad-030f72030e07", "AQAAAAIAAYagAAAAEDw/9DimUqVznh1+Kr0eYmiycx/yqy5GSe8cVS+1aCawA/EMg/Cn+gZ8sBmo9inJJg==", "d5021304-ad15-4a21-bde9-893a09a5ba22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6cb4f9c-d5e2-40d1-bd4b-984589d5387f", "AQAAAAIAAYagAAAAEMWmQZiPRX+g5zZQW+36IDw+ZfCVe8fUwCdnluTF8I+ILBFEm8xd12bJrxTVfc7CWQ==", "5c0a534c-a76e-4076-be94-30534f60254c" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_AspNetUsers_CustomerId",
                table: "ProductReviews",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductReviews_Products_ProductId",
                table: "ProductReviews",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
