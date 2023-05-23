using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.Backend.API.Migrations
{
    public partial class ExtendOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CookId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CourierId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RestaurantId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CookId",
                table: "Orders",
                column: "CookId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CourierId",
                table: "Orders",
                column: "CourierId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                table: "Orders",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cooks_CookId",
                table: "Orders",
                column: "CookId",
                principalTable: "Cooks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Restaurants_RestaurantId",
                table: "Orders",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cooks_CookId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Couriers_CourierId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Restaurants_RestaurantId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CookId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CourierId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_RestaurantId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CookId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Orders");
        }
    }
}
