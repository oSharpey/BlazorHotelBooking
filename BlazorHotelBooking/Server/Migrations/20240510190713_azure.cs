using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class azure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "49d8a498-e8c6-4ab1-bc7a-326c3bb5c542");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "bafa3719-ddf0-4928-bfb3-202c66a599fb");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3abb3c01-c296-4671-bda2-b80b831498e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "934ab536-61cd-4382-b51d-5c172389b892");
        }
    }
}
