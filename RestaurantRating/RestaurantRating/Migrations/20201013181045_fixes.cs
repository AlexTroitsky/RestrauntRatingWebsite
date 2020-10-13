using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantRating.Migrations
{
    public partial class fixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Restaurant",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Restaurant",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Restaurant");
        }
    }
}
