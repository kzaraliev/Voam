using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voam.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiry", "SecurityStamp" },
                values: new object[] { "02f2826a-78ff-4908-838f-401703f42a39", "AQAAAAIAAYagAAAAEJLpct+KN94zchMvTHdt6r2EAEUG5AlBFdjKW85M5XCsNEfL4q/gdrbNVnWYHISTow==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "e5976898-1c17-4c47-a131-f1a7e79630af" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiry", "SecurityStamp" },
                values: new object[] { "46d80da3-f98a-4904-bb0e-cc0bf4e0926d", "AQAAAAIAAYagAAAAEJ6qX4RGdhjokmwN4mAjlxKLWrBHuaqAWocmzWD2XQw8qC2nD7/jNoCwNNZ5iFBn2g==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "b6750c6c-d4ec-423b-a8be-466963089100" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiry",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a435102-3861-4312-8246-d459e77e8f4a", "AQAAAAIAAYagAAAAEENtDXYUD65FEomBvR9gZRc7EldFpZfDX//kq6WOytVeQ+7CgayjxaqzSYhMAKmdow==", "128e1ef7-427f-4b22-ad5d-b2ecf39e78aa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "543695a2-4979-44b4-840c-96eee7ec5701", "AQAAAAIAAYagAAAAEM1tdGLYdhLqHy/mKmQJJkIZNiTSej3z+NQFyiz78EVbCgu30g+HyQvwD8lDSnnd2A==", "b2a696dc-db31-4d84-8e67-f748666cf8c7" });
        }
    }
}
