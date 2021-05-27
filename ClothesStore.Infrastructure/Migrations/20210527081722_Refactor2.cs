using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothesStore.Infrastructure.Migrations
{
    public partial class Refactor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipAddress_ShipAddress",
                table: "Orders",
                newName: "ShipAddress");

            migrationBuilder.RenameColumn(
                name: "ShipAddress_PostalCode",
                table: "Orders",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "ShipAddress_City",
                table: "Orders",
                newName: "City");

            migrationBuilder.AlterColumn<string>(
                name: "ShipAddress",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShipAddress",
                table: "Orders",
                newName: "ShipAddress_ShipAddress");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Orders",
                newName: "ShipAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Orders",
                newName: "ShipAddress_City");

            migrationBuilder.AlterColumn<string>(
                name: "ShipAddress_ShipAddress",
                table: "Orders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "ShipAddress_PostalCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ShipAddress_City",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
