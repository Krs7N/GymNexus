using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymNexus.Infrastructure.Migrations
{
    public partial class RemovedUnnecessaryStoreIdFromOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stores_StoreId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StoreId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Orders");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "The unique identifier of the store from which the order was made");

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stores_StoreId",
                table: "Orders",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
