using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymNexus.Infrastructure.Migrations
{
    public partial class AddQuantityToEachProductInOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrdersDetails",
                type: "int",
                nullable: false,
                defaultValue: 1,
                comment: "The quantity of each product in the order");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrdersDetails");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "60889746-673c-4e86-8040-c4465e10c8b7",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17f0fe62-e94c-4dae-89d1-75581792d226", "AQAAAAEAACcQAAAAENodaB4ijMwVRnDIWQbZQBrgxh0YnDocj6e+jHkEWfy4On0RFpFrfwVfQpTalzarHQ==", "728d3196-6feb-476d-9560-dc4bc4143b0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76bb4713-0ad4-4e3e-a356-d3f24a435ec9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e654fca-7d3b-445c-b5f5-00cfcec1e4a1", "AQAAAAEAACcQAAAAEAvOYmgVnB94fXgOy5cg2N8Hf+4PYjqdmfy45JCz5qenO73jd/ihgQ6+n2TOzzDzOA==", "4dec10e5-3d07-43c2-9ba0-ab4ec7c51c56" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 18, 9, 3, 797, DateTimeKind.Local).AddTicks(6437));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 3, 18, 9, 3, 797, DateTimeKind.Local).AddTicks(6476));

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2023, 4, 3, 18, 9, 3, 797, DateTimeKind.Local).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 3, 29, 18, 9, 3, 797, DateTimeKind.Local).AddTicks(8038));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 2, 3, 18, 9, 3, 797, DateTimeKind.Local).AddTicks(8046));

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 4, 3, 18, 9, 3, 797, DateTimeKind.Local).AddTicks(7309));
        }
    }
}
