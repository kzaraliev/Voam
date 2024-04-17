using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voam.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddMaxLengthReviewAndProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3021968f-c614-428b-ab60-d4670f00394a", "AQAAAAIAAYagAAAAEK8EP8LI6Juoc8EX6DNJL7N5PpmagnEdBORLCY1PiaFq1LOOZh2M/U1JtVtycypcPg==", "f7e53fdc-8a10-4cbe-bba5-205442313d18" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "479e1860-ccc3-49f3-96cf-d05a37b6e7ba", "AQAAAAIAAYagAAAAEEmZFYcbY5kKvcWJfmwNUBzgBt1KHzzPkQdM0yg8VxecGVsWXse3VpQFEp+OzTVQTw==", "40f43fd7-b4b4-4bb9-bc00-15bd08185319" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "02f2826a-78ff-4908-838f-401703f42a39", "AQAAAAIAAYagAAAAEJLpct+KN94zchMvTHdt6r2EAEUG5AlBFdjKW85M5XCsNEfL4q/gdrbNVnWYHISTow==", "e5976898-1c17-4c47-a131-f1a7e79630af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "46d80da3-f98a-4904-bb0e-cc0bf4e0926d", "AQAAAAIAAYagAAAAEJ6qX4RGdhjokmwN4mAjlxKLWrBHuaqAWocmzWD2XQw8qC2nD7/jNoCwNNZ5iFBn2g==", "b6750c6c-d4ec-423b-a8be-466963089100" });
        }
    }
}
