using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymNexus.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "Application user entity representation in the system. Extends the default IdentityUser");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "The URL representation of the user's profile picture",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the category")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The name of the category"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The description of the category"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "The status of the category. If it is active or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                },
                comment: "Category entity representation in the system");

            migrationBuilder.CreateTable(
                name: "Marketplaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the marketplace")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The name of the marketplace"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The description of the marketplace"),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false, comment: "The address of the marketplace"),
                    Latitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false, comment: "The latitude coordinate representation of the marketplace"),
                    Longitude = table.Column<decimal>(type: "decimal(12,9)", nullable: false, comment: "The longitude coordinate representation of the marketplace"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "The status of the marketplace. If it is active or not")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marketplaces", x => x.Id);
                },
                comment: "Marketplace entity representation in the system");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the post")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "The title of the post"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The content of the post"),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "The URL representation of the post's image. Post could have no image inserted"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "The status of the post. If it is active or not. Set to true by default"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the post was added to the system"),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the creator of the post")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Post entity representation in the system");

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the store")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "The name of the store"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The description of the store"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the store was added to the system, and created"),
                    AverageRating = table.Column<decimal>(type: "decimal(2,2)", nullable: false, comment: "The average rating that the store has received up to this moment"),
                    RatingsCount = table.Column<int>(type: "int", nullable: false, comment: "The count of all ratings that the store has received up to this moment"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "The status of the store. If it is active or not"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the store's owner"),
                    MarketplaceId = table.Column<int>(type: "int", nullable: true, comment: "The unique identifier of the store's marketplace. The store initially starts without a marketplace and can continue be without one")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stores_Marketplaces_MarketplaceId",
                        column: x => x.MarketplaceId,
                        principalTable: "Marketplaces",
                        principalColumn: "Id");
                },
                comment: "Store entity representation in the system");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the comment")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "The content of the comment"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "The status of the comment. Represents if it is active or not. Comments could be deleted"),
                    PostId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the post that the comment is related to"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the comment was added to the system"),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the creator of the comment. It is set on creation of the comment")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Comment entity representation in the system");

            migrationBuilder.CreateTable(
                name: "PostsLikes",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the post"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the user who liked the post")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostsLikes", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostsLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostsLikes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Post like entity representation in the system. Represents the amount of likes the post has received");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the order")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the store from which the order was made"),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the user who made the order"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the order was made. Set on creation of the order"),
                    UnitPrice = table.Column<decimal>(type: "decimal(4,2)", nullable: false, comment: "The price for a unit in the order"),
                    Quantity = table.Column<int>(type: "int", nullable: false, comment: "The quantity of the products in the order"),
                    TotalPrice = table.Column<decimal>(type: "decimal(9,2)", nullable: false, comment: "The total price of the order"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "The status of the order"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "The payment method for the order"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "The status of the order. Represents if it is active or not. Set to true by default")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Order entity representation in the system");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the product")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, comment: "The name of the product"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "The description of the product"),
                    ImageUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "The URL representation of the product's image"),
                    Price = table.Column<decimal>(type: "decimal(4,2)", nullable: false, comment: "The price of the product"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date and time when the product was added to the system"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "Determines whether the product is still active in the system or not"),
                    StoreId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the store that is selling the current product"),
                    CategoryId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the category that the product belongs to")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Product entity representation in a user's store");

            migrationBuilder.CreateTable(
                name: "OrdersDetails",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the order"),
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the product that is included in the order")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrdersDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdersDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "The order details of an order with the different products entity representation in the system");

            migrationBuilder.CreateTable(
                name: "ProductsLikes",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false, comment: "The unique identifier of the product"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The unique identifier of the user who liked the product")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsLikes", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProductsLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsLikes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Product like entity representation in the system. Represents the amount of likes the product has received");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fc7efe55-f532-4b76-91bf-2ebfe9c724f8", 0, "5beb2c37-abbf-46a6-9508-4f5de362bf8e", null, false, false, null, null, "ROOT@ABV.BG", "AQAAAAEAACcQAAAAEAoSCJ/94nh7fHUTiwPcufBpg+0zFIGJDynrLR3hPq8QnudXJhQmAB2O7D6U/yrs8Q==", null, false, null, "27e19282-8a49-4a77-bff3-1207f1b1f014", false, "root@abv.bg" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Special whey protein made by Kevin Levrone's own brand", true, "Protein Whey" },
                    { 2, "This is the default brand of proteins that we can give you for now", true, "Protein" },
                    { 3, "Creatine is a substance that is found in small amounts in the body. It is also found in certain foods and can be taken as a dietary supplement. Creatine is involved in producing the energy that muscles need to work.", true, "Creatine" },
                    { 4, "Creatine Monohydrate is of the more advanced types of creatine out there. It is one of the most researched supplements worldwide and can help the brain activity!", true, "Creatine Monohydrate" }
                });

            migrationBuilder.InsertData(
                table: "Marketplaces",
                columns: new[] { "Id", "Address", "Description", "IsActive", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { 1, "Boulevard \"Cherni vrah\" 25, Sofia", "Fitness1 is a marketplace that offers a wide range of fitness products and supplements.", true, 42.6777m, 23.3221m, "Fitness1 Sofia" },
                    { 2, "Mladost 4, 1715, Sofia", "Pulse Gym Shop offers various supplements and gym equipment. The store is part of the Pulse brand which has its own gym's all around the country", true, 42.62518m, 23.373451m, "Pulse Gym Shop" },
                    { 3, "Lyuben Karavelov 21, 9002, Varna", "Sila BG is one of the leading brands in Bulgaria. A recent new-comer but with a high demand with various range of products.", true, 43.20887m, 27.92242m, "Sila BG" },
                    { 4, "Adam Mitskevich 5, 8001, Burgas", "Fitness1 is a marketplace that offers a wide range of fitness products and supplements.", true, 42.50064m, 27.47921m, "Fitness1 Burgas" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "CreatedBy", "CreatedOn", "ImageUrl", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, "Welcome to GymNexus! This is a social network for fitness enthusiasts. Share your progress, ask for advice, and connect with other people who share your passion for fitness.", "fc7efe55-f532-4b76-91bf-2ebfe9c724f8", new DateTime(2024, 2, 29, 15, 58, 34, 642, DateTimeKind.Local).AddTicks(1276), null, true, "Welcome to GymNexus" },
                    { 2, "To get started, create an account and start sharing your fitness journey with the world. You can also connect with other users and see their progress.", "fc7efe55-f532-4b76-91bf-2ebfe9c724f8", new DateTime(2024, 1, 5, 15, 58, 34, 642, DateTimeKind.Local).AddTicks(1304), null, true, "How to get started" },
                    { 3, "I am looking to start increasing my bench press and bench more, and put more pressure on my chest muscles. I am looking for advices, thanks in advance!", "fc7efe55-f532-4b76-91bf-2ebfe9c724f8", new DateTime(2023, 3, 5, 15, 58, 34, 642, DateTimeKind.Local).AddTicks(1356), null, true, "How can I bench more?" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "AverageRating", "CreatedOn", "Description", "IsActive", "MarketplaceId", "Name", "OwnerId", "RatingsCount" },
                values: new object[] { 1, 0m, new DateTime(2024, 3, 5, 15, 58, 34, 642, DateTimeKind.Local).AddTicks(2027), "This is the Root's store that is created to be useful for you and easier to start with. It is owned by the root user.", true, 1, "Root's local Gym Shop", "fc7efe55-f532-4b76-91bf-2ebfe9c724f8", 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Name", "Price", "StoreId" },
                values: new object[] { 1, 1, new DateTime(2024, 2, 29, 15, 58, 34, 642, DateTimeKind.Local).AddTicks(2420), "Special whey protein made by Kevin Levrone's own brand. Comes in 2000 grams package, with a spoon that is 30g and recommended daily usage of 30g", "https://www.kevinlevrone.com/wp-content/uploads/2021/06/levrone-whey-protein-2000g.jpg", true, "Kevin Levrone's Whey Protein", 50.00m, 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "Description", "ImageUrl", "IsActive", "Name", "Price", "StoreId" },
                values: new object[] { 2, 2, new DateTime(2024, 1, 5, 15, 58, 34, 642, DateTimeKind.Local).AddTicks(2424), "This is the default brand of proteins that we can give you for now. Comes in 1000 grams package, with a spoon that is 30g and recommended daily usage of 30g", "https://gymbeam.bg/media/catalog/product/cache/bf5a31e851f50f3ed6850cbbf183db11/j/u/just_whey_chocolate_milkshake_1_kg_gymbeam_1.png", true, "Protein", 26.00m, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedBy",
                table: "Comments",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedBy",
                table: "Orders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StoreId",
                table: "Orders",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetails_ProductId",
                table: "OrdersDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedBy",
                table: "Posts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PostsLikes_UserId",
                table: "PostsLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsLikes_UserId",
                table: "ProductsLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_MarketplaceId",
                table: "Stores",
                column: "MarketplaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_OwnerId",
                table: "Stores",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "OrdersDetails");

            migrationBuilder.DropTable(
                name: "PostsLikes");

            migrationBuilder.DropTable(
                name: "ProductsLikes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Marketplaces");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fc7efe55-f532-4b76-91bf-2ebfe9c724f8");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "Application user entity representation in the system. Extends the default IdentityUser");

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "AspNetUsers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "The URL representation of the user's profile picture");
        }
    }
}
