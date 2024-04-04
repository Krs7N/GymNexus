using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymNexus.Infrastructure.Migrations
{
    public partial class RemoveUnitPriceFromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60889746-673c-4e86-8040-c4465e10c8b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1059de9a-6e57-46b9-a59c-0d0006744836", "AQAAAAEAACcQAAAAEOWiVbWv+gnOMLWi918NzHa5q0Itt1TsQH6hfIqjjlepkYAahg+OSkRIspB0gOso0w==", "3dfa1ec0-b777-49e6-8ac7-3cd81a8fdf3d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76bb4713-0ad4-4e3e-a356-d3f24a435ec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da3e91e3-d2cc-45b3-9ebe-99d7c54433c7", "AQAAAAEAACcQAAAAECbru3C77P9xCIkJW90NIdk5G+EzsHjYkkVGGB3e4dfflKrOXlgaCwRPSVOk6TVPng==", "f28b0f8c-18d9-4a35-9ec9-d59cc615072e" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 42, 58, 190, DateTimeKind.Local).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 3, 16, 42, 58, 190, DateTimeKind.Local).AddTicks(2321));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 3, 16, 42, 58, 190, DateTimeKind.Local).AddTicks(2324));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 16, 42, 58, 190, DateTimeKind.Local).AddTicks(3775));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 3, 16, 42, 58, 190, DateTimeKind.Local).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 3, 16, 42, 58, 190, DateTimeKind.Local).AddTicks(3295));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Orders",
                type: "decimal(4,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "The price for a unit in the order");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60889746-673c-4e86-8040-c4465e10c8b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "662ef879-4b39-4569-97ed-22cafa40fd99", "AQAAAAEAACcQAAAAEFpIQiCQjPRUCr2gIIqnKnqCDJKSy4vN9rnZOnm3qpFnEq0MoTey/vxnrWpMr76lSQ==", "e4f7bea9-0cfd-4ee6-902a-5b6c2437489a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76bb4713-0ad4-4e3e-a356-d3f24a435ec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a479101-2ede-443d-94fa-7505c882f7f0", "AQAAAAEAACcQAAAAEMZ8ngBB0nc+njAE28pIYRgw8bAXzH809diQKXFiVN02zpaYAlTTYz0IE8CrwbULqA==", "93db9f65-88dd-401b-90e9-1ad7cae96a17" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 8, 16, 43, 36, 243, DateTimeKind.Local).AddTicks(3375));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 13, 16, 43, 36, 243, DateTimeKind.Local).AddTicks(3402));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 3, 13, 16, 43, 36, 243, DateTimeKind.Local).AddTicks(3405));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 8, 16, 43, 36, 243, DateTimeKind.Local).AddTicks(4557));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 13, 16, 43, 36, 243, DateTimeKind.Local).AddTicks(4562));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 13, 16, 43, 36, 243, DateTimeKind.Local).AddTicks(4110));
        }
    }
}
