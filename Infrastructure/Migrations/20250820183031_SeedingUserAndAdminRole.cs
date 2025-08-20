using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingUserAndAdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "IsDefault", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019791d7-92ab-7022-8774-1eecdba1f2a2", "019791d9c60a7d889dab3f7809e740f3", "ApplicationRole", false, "Admin", "ADMIN" },
                    { "019791d7-da52-70db-96ec-87c909bc811a", "019791d864df7179a5d6d15c06eb1e83", "ApplicationRole", true, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "MembershipStatus", "MembershipType", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019791ae-3c34-726a-8b42-c4b2a8f511ed", 0, "", "019791af89de76b5928115f69be7e708", "Admin@Library.com", true, "Survey Basket", "Admin", false, null, "", "", "ADMIN@LIBRARY.COM", "ADMIN@LIBRARY.COM", "AQAAAAIAAYagAAAAEFoBZtZrBXuTETtpN2af//gQ1MSZXe55JL1W9DvO/E9344/QpahxmhAMcjCkA7Zrqg==", null, false, "019791af-20da-7916-a88b-06f1dbc5ab92", false, "Admin@Library.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019791d7-92ab-7022-8774-1eecdba1f2a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019791d7-da52-70db-96ec-87c909bc811a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019791ae-3c34-726a-8b42-c4b2a8f511ed");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "AspNetRoles");
        }
    }
}
