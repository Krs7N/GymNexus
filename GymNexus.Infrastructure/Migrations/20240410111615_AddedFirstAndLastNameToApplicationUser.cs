using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymNexus.Infrastructure.Migrations
{
    public partial class AddedFirstAndLastNameToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true,
                comment: "The first name of the user");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                comment: "The last name of the user");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60889746-673c-4e86-8040-c4465e10c8b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3276fac9-102c-4b7f-8ae3-8c0a6aca9e81", "AQAAAAEAACcQAAAAEDATEBWZT+JEZwJxUt11Mk2JCg2ygW2rCWbZ60l4BAqLwhovtVh/I0TZi0cT2J5vmg==", "8783bd40-abbd-452a-b358-3467496f86f7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76bb4713-0ad4-4e3e-a356-d3f24a435ec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "205096e1-1a56-4d13-9e94-39590011f9d3", "AQAAAAEAACcQAAAAEM8gIRN0d393yDOdI47J7GLE3vhfH7S7Eermn7N3QYk5wHwMhlqJ2HWE/xGkXcl4BQ==", "f2349e73-4daa-4c0b-b2d8-585250f2ca5f" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 5, 14, 16, 15, 210, DateTimeKind.Local).AddTicks(3979));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 10, 14, 16, 15, 210, DateTimeKind.Local).AddTicks(4008));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 10, 14, 16, 15, 210, DateTimeKind.Local).AddTicks(4011));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 5, 14, 16, 15, 210, DateTimeKind.Local).AddTicks(5035));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 10, 14, 16, 15, 210, DateTimeKind.Local).AddTicks(5039));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 10, 14, 16, 15, 210, DateTimeKind.Local).AddTicks(4601));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60889746-673c-4e86-8040-c4465e10c8b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83c5d6e6-7cdc-453f-9b6a-f6e756275e7e", "AQAAAAEAACcQAAAAEFVYIbsBGhA2wsni4Vd3tQK+2YnmE5qPyJ+4BYf05YNNmLY+V5E6ZjCFJ00VI1T2rQ==", "7c87de15-864a-4450-bd44-5ec4bb52fd22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76bb4713-0ad4-4e3e-a356-d3f24a435ec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2f490e4-1996-4275-a53a-96e7a8b0423f", "AQAAAAEAACcQAAAAEGDuFPBdUtwhJ66rjC5DZD2Hfrn3UkMF2P8W0sZmPchKKD2HYMeysq1kXNrD0bw13g==", "6fff3ebf-2bbb-40ca-a008-7d54ec7f2308" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 9, 39, 55, 485, DateTimeKind.Local).AddTicks(782));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 4, 9, 39, 55, 485, DateTimeKind.Local).AddTicks(811));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 4, 9, 39, 55, 485, DateTimeKind.Local).AddTicks(814));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 30, 9, 39, 55, 485, DateTimeKind.Local).AddTicks(2396));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 4, 9, 39, 55, 485, DateTimeKind.Local).AddTicks(2400));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 4, 9, 39, 55, 485, DateTimeKind.Local).AddTicks(1767));
        }
    }
}
