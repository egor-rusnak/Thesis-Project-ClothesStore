using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothesStore.Infrastructure.Migrations
{
    public partial class badumtsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imgSrc",
                table: "ClothesTypes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ClothesTypes",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Clothes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ClothesTypes");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Clothes");

            migrationBuilder.AddColumn<string>(
                name: "imgSrc",
                table: "ClothesTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
