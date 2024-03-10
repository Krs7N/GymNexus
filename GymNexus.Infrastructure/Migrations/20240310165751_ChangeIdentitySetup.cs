using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymNexus.Infrastructure.Migrations
{
    public partial class ChangeIdentitySetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60889746-673c-4e86-8040-c4465e10c8b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6da6e9fa-2bb5-46fe-8d68-0ee6fda1a107", "AQAAAAEAACcQAAAAEJ/9k+sqzBnwfCZ2Cf4QL6mWcR91uZZgjDjA2eM0j628dFaeY0Yp9su16KBGdsw5yQ==", "fa31ab9f-09c1-4e00-a6d9-4c2b69b211d1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76bb4713-0ad4-4e3e-a356-d3f24a435ec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3a06b9b-74cf-4c2e-b945-c7278ba64a74", "AQAAAAEAACcQAAAAEIIGL3r88B6uKrsJWX/LgzJA0O5TOJ9zSXRDS6WDLWX7vmu+5jZLyaVTodITqdnkeg==", "89df91af-0aac-4c1f-9d89-f86a72bfbcc3" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 5, 18, 57, 51, 290, DateTimeKind.Local).AddTicks(9764));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 10, 18, 57, 51, 290, DateTimeKind.Local).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 3, 10, 18, 57, 51, 290, DateTimeKind.Local).AddTicks(9801));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 5, 18, 57, 51, 291, DateTimeKind.Local).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 10, 18, 57, 51, 291, DateTimeKind.Local).AddTicks(1159));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 10, 18, 57, 51, 291, DateTimeKind.Local).AddTicks(634));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60889746-673c-4e86-8040-c4465e10c8b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6148cbdc-31c9-4868-aff9-85935540478f", "AQAAAAEAACcQAAAAEBnG3rhSmwEo8a7C31JLBZnddZu12KC7BsFlampz/glPqDkmmgKs5xa7e1XxYN8GXQ==", "45dbf073-7416-458a-8a49-c7c4415793d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76bb4713-0ad4-4e3e-a356-d3f24a435ec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "160e64f8-e545-43a0-bc3b-b0caa71b9f24", "AQAAAAEAACcQAAAAEPNG7Rl1vqS5uVRyTEcQ3uiTiy3/akA7aQ75UYjCjijGc68lyAhU6VhIY7oLXwMvtw==", "2f918d4e-39b5-4a84-9618-7f0454b870aa" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 2, 15, 34, 22, 992, DateTimeKind.Local).AddTicks(7882));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 7, 15, 34, 22, 992, DateTimeKind.Local).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 3, 7, 15, 34, 22, 992, DateTimeKind.Local).AddTicks(7941));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 2, 15, 34, 22, 992, DateTimeKind.Local).AddTicks(9074));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 1, 7, 15, 34, 22, 992, DateTimeKind.Local).AddTicks(9080));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 7, 15, 34, 22, 992, DateTimeKind.Local).AddTicks(8615));
        }
    }
}
