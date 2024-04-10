using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Voam.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "City", "CustomerId", "Econt", "Email", "FullName", "OrderDate", "PaymentMethod", "PhoneNumber", "TotalPrice" },
                values: new object[,]
                {
                    { 1, "София", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "булевард „Александър Стамболийски“ №215, 215 217, 1213 София", "kosebose@mail.com", "Konstantin Zaraliev", new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upon delivery", "0876933660", 125m },
                    { 2, "София", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "ж.к. Сердика, ул. „Гюешево“ №83, 1000 София", "kosebose@mail.com", "Konstantin Zaraliev", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upon delivery", "0876933660", 75m },
                    { 3, "София", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "НПЗ Военна рампа, бул. „Илиянци“ №12, 1062 София", "kosebose@mail.com", "Konstantin Zaraliev", new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upon delivery", "0876933660", 200m },
                    { 4, "Пловдив", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "Район, ул. „Беласица“ №49, 4000 Пловдив", "kosebose@mail.com", "Konstantin Zaraliev", new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upon delivery", "0876933660", 160m },
                    { 5, "Пловдив", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "Район Южен, ул, Бл. „Кукленско шосе“ №11, 4004 Пловдив", "kosebose@mail.com", "Konstantin Zaraliev", new DateTime(2024, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upon delivery", "0876933660", 80m },
                    { 6, "Велико Търново", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "Район Западен, ул. „Мизия“ №28, 5002 Велико Търново", "kosebose@mail.com", "Konstantin Zaraliev", new DateTime(2024, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upon delivery", "0876933660", 80m },
                    { 7, "Велико Търново", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", "ж.к. Зона Б, ул. „Стоян Коледаров“ №44, 5000 Велико Търново", "kosebose@mail.com", "Konstantin Zaraliev", new DateTime(2024, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Upon delivery", "0876933660", 80m }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "Name", "OrderId", "Price", "Quantity", "SizeChar" },
                values: new object[,]
                {
                    { 1, "“DEVOTION” T-SHIRT", 1, 50m, 2, "S" },
                    { 2, "“DEVOTION” BEANIE", 1, 25m, 1, "M" },
                    { 3, "“DEVOTION” BEANIE", 2, 25m, 3, "M" },
                    { 4, "MIDNIGHT GREEN HOODIE", 3, 100m, 1, "L" },
                    { 5, "MIDNIGHT GREEN PANTS", 3, 100m, 1, "L" },
                    { 6, "“BLACK SHEEP” HOODIE", 4, 80m, 2, "S" },
                    { 7, "“BLACK SHEEP” HOODIE", 5, 80m, 1, "L" },
                    { 8, "“VOAM*” EMBROIDERY HOODIE", 6, 80m, 1, "L" },
                    { 9, "“VOAM*” EMBROIDERY HOODIE", 7, 80m, 1, "S" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

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
        }
    }
}
