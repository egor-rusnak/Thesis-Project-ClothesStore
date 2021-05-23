using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothesStore.Infrastructure.Migrations
{
    public partial class badum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Finished",
                table: "Orders",
                newName: "Shiped");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Shiped",
                table: "Orders",
                newName: "Finished");
        }
    }
}
