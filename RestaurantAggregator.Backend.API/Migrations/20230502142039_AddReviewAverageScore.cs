using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.API.Migrations
{
    public partial class AddReviewAverageScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewAverageScore",
                table: "Dishes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewAverageScore",
                table: "Dishes");
        }
    }
}
