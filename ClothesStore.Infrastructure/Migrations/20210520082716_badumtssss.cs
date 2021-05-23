using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothesStore.Infrastructure.Migrations
{
    public partial class badumtssss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ClothesTypes");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ClothesTypes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ClothesTypes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ClothesTypes",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
