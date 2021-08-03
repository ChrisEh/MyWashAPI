using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodApi.Migrations
{
    public partial class addHouseNumberToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HouseNumber",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNumber",
                table: "Users");
        }
    }
}
