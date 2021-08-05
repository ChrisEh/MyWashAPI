using Microsoft.EntityFrameworkCore.Migrations;

namespace MyWashApi.Data.Migrations
{
    public partial class pickupStatusAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PickupStatus",
                table: "Pickups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickupStatus",
                table: "Pickups");
        }
    }
}
