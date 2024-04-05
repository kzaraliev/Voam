using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voam.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneNumberSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "0c3cf8d1-730c-462a-a4ad-030f72030e07", "AQAAAAIAAYagAAAAEDw/9DimUqVznh1+Kr0eYmiycx/yqy5GSe8cVS+1aCawA/EMg/Cn+gZ8sBmo9inJJg==", "0876933660", "d5021304-ad15-4a21-bde9-893a09a5ba22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "a6cb4f9c-d5e2-40d1-bd4b-984589d5387f", "AQAAAAIAAYagAAAAEMWmQZiPRX+g5zZQW+36IDw+ZfCVe8fUwCdnluTF8I+ILBFEm8xd12bJrxTVfc7CWQ==", "0876933440", "5c0a534c-a76e-4076-be94-30534f60254c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "46a9d1de-c96d-4916-9f8a-30f3613ae91e", "AQAAAAIAAYagAAAAEEA1sBtsHfsXImeoPQZ8jtZ/Qz8id/eT+0hL0y6RCKgi28g4dLvk5U+XfM8WrZMfwA==", null, "3797fda0-3f52-4e96-9fee-687c758c459e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "0891a5f6-8c8c-4ee3-8d9b-cbe49adcbb41", "AQAAAAIAAYagAAAAEEtEsxFtCrYTpVnBtDK3M/DeSCqJGMpT+kB5r0P4E4BduWLZKudCDNk5f4ob5UKc2g==", null, "f22c11a4-5158-4c8b-87ab-9facfccf52ae" });
        }
    }
}
