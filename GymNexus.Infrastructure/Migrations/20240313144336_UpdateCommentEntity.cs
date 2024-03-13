using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymNexus.Infrastructure.Migrations
{
    public partial class UpdateCommentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "The edit status of the comment. Represents if it was edited at any point.");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "Comments");

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
    }
}
