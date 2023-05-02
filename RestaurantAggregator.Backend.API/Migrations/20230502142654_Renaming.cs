using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.API.Migrations
{
    public partial class Renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewAverageScore",
                table: "Dishes",
                newName: "ReviewsAverageScore");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReviewsAverageScore",
                table: "Dishes",
                newName: "ReviewAverageScore");
        }
    }
}
