using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.Auth.API.Migrations
{
    public partial class RemodeExtraData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForTest",
                table: "Cooks");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4482297d-6e7e-40d7-9ad1-78dffd3942f0"),
                column: "ConcurrencyStamp",
                value: "d61b7140-cbb6-473a-9337-83963f099373");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ab6e5fd-8008-47d1-879f-3e197f67670c"),
                column: "ConcurrencyStamp",
                value: "a68f6a00-f392-4e3e-8d05-0e2397c66cf0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                column: "ConcurrencyStamp",
                value: "408d8f10-e9ce-4057-8404-1288f08c2244");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"),
                column: "ConcurrencyStamp",
                value: "c8b762da-8249-4800-9995-9ecf0282fed2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ForTest",
                table: "Cooks",
                type: "text",
                nullable: false,
                defaultValue: "");

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
    }
}
