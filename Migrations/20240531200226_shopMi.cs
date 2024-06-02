using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainProject.Migrations
{
    /// <inheritdoc />
    public partial class shopMi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeathTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "WareGroups",
                columns: table => new
                {
                    WareGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WareGroupName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareGroups", x => x.WareGroupID);
                });

            migrationBuilder.CreateTable(
                name: "WareSubgroups",
                columns: table => new
                {
                    WareSubGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WareSubGroupName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WareGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WareSubgroups", x => x.WareSubGroupID);
                    table.ForeignKey(
                        name: "FK_WareSubgroups_WareGroups_WareGroupID",
                        column: x => x.WareGroupID,
                        principalTable: "WareGroups",
                        principalColumn: "WareGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wares",
                columns: table => new
                {
                    WareID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WareGroupID = table.Column<int>(type: "int", nullable: false),
                    WareSubGroupID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(95)", maxLength: 95, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    DateOfInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Walpapers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Visit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wares", x => x.WareID);
                    table.ForeignKey(
                        name: "FK_Wares_WareSubgroups_WareSubGroupID",
                        column: x => x.WareSubGroupID,
                        principalTable: "WareSubgroups",
                        principalColumn: "WareSubGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    WareID = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    DelivaryPlace = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Wares_WareID",
                        column: x => x.WareID,
                        principalTable: "Wares",
                        principalColumn: "WareID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopingCarts",
                columns: table => new
                {
                    ShopingCartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    WareID = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopingCarts", x => x.ShopingCartID);
                    table.ForeignKey(
                        name: "FK_ShopingCarts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopingCarts_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WareID",
                table: "Orders",
                column: "WareID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopingCarts_CustomerID",
                table: "ShopingCarts",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ShopingCarts_OrderID",
                table: "ShopingCarts",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Wares_WareSubGroupID",
                table: "Wares",
                column: "WareSubGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_WareSubgroups_WareGroupID",
                table: "WareSubgroups",
                column: "WareGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopingCarts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Wares");

            migrationBuilder.DropTable(
                name: "WareSubgroups");

            migrationBuilder.DropTable(
                name: "WareGroups");
        }
    }
}
