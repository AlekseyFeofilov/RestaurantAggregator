using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.Auth.API.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Type" },
                values: new object[,]
                {
                    { new Guid("4482297d-6e7e-40d7-9ad1-78dffd3942f0"), "23780879-aff7-49f0-8ae2-b0cb209d656a", "Cook", "COOK", 0 },
                    { new Guid("7ab6e5fd-8008-47d1-879f-3e197f67670c"), "5cd1665d-b59b-42d3-aebe-b92bf43b9162", "Manager", "MANAGER", 3 },
                    { new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26"), "bcf88586-4c2e-4359-84aa-2760d23ec17b", "Customer", "CUSTOMER", 2 },
                    { new Guid("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"), "9b8289eb-bc37-47f9-aae9-aab613125853", "Courier", "COURIER", 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "FirstTestType", null, new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26") },
                    { 2, "SecondTestType", null, new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4482297d-6e7e-40d7-9ad1-78dffd3942f0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ab6e5fd-8008-47d1-879f-3e197f67670c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26"));
        }
    }
}
