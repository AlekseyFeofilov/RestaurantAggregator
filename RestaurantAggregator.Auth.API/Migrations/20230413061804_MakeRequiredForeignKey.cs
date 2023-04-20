using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.Auth.API.Migrations
{
    public partial class MakeRequiredForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4482297d-6e7e-40d7-9ad1-78dffd3942f0"),
                column: "ConcurrencyStamp",
                value: "cbcd14e1-8570-4c6c-bf0e-3eefae20e67a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ab6e5fd-8008-47d1-879f-3e197f67670c"),
                column: "ConcurrencyStamp",
                value: "858a18f6-ef11-4cf0-8e38-b3f1c7248639");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                column: "ConcurrencyStamp",
                value: "e0052225-c736-49e0-a788-771a905d4bb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"),
                column: "ConcurrencyStamp",
                value: "1db09ec9-4a1a-4704-ad19-b99cb1273a1d");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4482297d-6e7e-40d7-9ad1-78dffd3942f0"),
                column: "ConcurrencyStamp",
                value: "9aaebe95-f8ee-4006-af18-ccee3e4d4394");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ab6e5fd-8008-47d1-879f-3e197f67670c"),
                column: "ConcurrencyStamp",
                value: "d19645e4-81cd-4d37-ab6d-83bbe30e48d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                column: "ConcurrencyStamp",
                value: "deb0eb39-0e2a-453a-b45a-0d0ec07ba282");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"),
                column: "ConcurrencyStamp",
                value: "832e4878-1e07-4687-bc80-5eada3379c35");
        }
    }
}
