using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAggregator.Auth.API.Migrations
{
    public partial class RemoveUserRoleOverriding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserRoles");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForTest",
                table: "Cooks");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId1",
                table: "AspNetUserRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "AspNetUserRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4482297d-6e7e-40d7-9ad1-78dffd3942f0"),
                column: "ConcurrencyStamp",
                value: "23780879-aff7-49f0-8ae2-b0cb209d656a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7ab6e5fd-8008-47d1-879f-3e197f67670c"),
                column: "ConcurrencyStamp",
                value: "5cd1665d-b59b-42d3-aebe-b92bf43b9162");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93eb91dd-e76a-45d9-acc8-6c519e742f26"),
                column: "ConcurrencyStamp",
                value: "bcf88586-4c2e-4359-84aa-2760d23ec17b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("dcbbcfd3-554f-4574-a9c0-dee956c5ef2a"),
                column: "ConcurrencyStamp",
                value: "9b8289eb-bc37-47f9-aae9-aab613125853");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
