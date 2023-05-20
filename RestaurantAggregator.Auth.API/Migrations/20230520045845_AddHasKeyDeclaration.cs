using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.Auth.API.Migrations
{
    public partial class AddHasKeyDeclaration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Adress",
                table: "Customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4482297d-6e7e-40d7-9ad1-78dffd3942f0"),
                column: "ConcurrencyStamp",
                value: "7a99a067-64b7-4ad9-8dbc-cd3718caaab0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ab6e5fd-8008-47d1-879f-3e197f67670c"),
                column: "ConcurrencyStamp",
                value: "e4ebfa76-bbca-4e88-a86f-78c85d2db6a2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                column: "ConcurrencyStamp",
                value: "18578729-6326-4216-8450-f67db93f5309");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"),
                column: "ConcurrencyStamp",
                value: "ee0012fd-6e3e-49fe-817d-5e02a7ec6f08");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Adress",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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
    }
}
