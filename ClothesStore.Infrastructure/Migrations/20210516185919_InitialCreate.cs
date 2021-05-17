using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothesStore.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClothesTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destinantion = table.Column<int>(type: "int", nullable: false),
                    imgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesTypes", x => new { x.Destinantion, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clothes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    PromoutionPercent = table.Column<float>(type: "real", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClothesTypeDestinantion = table.Column<int>(type: "int", nullable: true),
                    ClothesTypeName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clothes_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clothes_ClothesTypes_ClothesTypeDestinantion_ClothesTypeName",
                        columns: x => new { x.ClothesTypeDestinantion, x.ClothesTypeName },
                        principalTable: "ClothesTypes",
                        principalColumns: new[] { "Destinantion", "Name" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayMethod = table.Column<int>(type: "int", nullable: false),
                    DateOfOrder = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    ShipAddress_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipAddress_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShipAddress_ShipAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Canceled = table.Column<bool>(type: "bit", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Managers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClothesMarks",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    ClothesId = table.Column<int>(type: "int", nullable: false),
                    CountInStock = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesMarks", x => new { x.ClothesId, x.SizeId });
                    table.ForeignKey(
                        name: "FK_ClothesMarks_Clothes_ClothesId",
                        column: x => x.ClothesId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothesMarks_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothesOrders",
                columns: table => new
                {
                    ClothesUnitId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    CostPerSingle = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ClothesUnitClothesId = table.Column<int>(type: "int", nullable: false),
                    ClothesUnitSizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothesOrders", x => new { x.ClothesUnitId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_ClothesOrders_ClothesMarks_ClothesUnitClothesId_ClothesUnitSizeId",
                        columns: x => new { x.ClothesUnitClothesId, x.ClothesUnitSizeId },
                        principalTable: "ClothesMarks",
                        principalColumns: new[] { "ClothesId", "SizeId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClothesOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_BrandId",
                table: "Clothes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_ClothesTypeDestinantion_ClothesTypeName",
                table: "Clothes",
                columns: new[] { "ClothesTypeDestinantion", "ClothesTypeName" });

            migrationBuilder.CreateIndex(
                name: "IX_ClothesMarks_SizeId",
                table: "ClothesMarks",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClothesOrders_ClothesUnitClothesId_ClothesUnitSizeId",
                table: "ClothesOrders",
                columns: new[] { "ClothesUnitClothesId", "ClothesUnitSizeId" });

            migrationBuilder.CreateIndex(
                name: "IX_ClothesOrders_OrderId",
                table: "ClothesOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ManagerId",
                table: "Orders",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothesOrders");

            migrationBuilder.DropTable(
                name: "ClothesMarks");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clothes");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "ClothesTypes");
        }
    }
}
