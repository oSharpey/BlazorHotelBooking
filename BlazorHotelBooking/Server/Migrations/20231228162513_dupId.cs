using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class dupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "duplicateId",
                table: "HotelBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "30c65597-e1ee-4d3e-8087-7d2f63435de4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1f88b17b-89c2-4e35-9428-c9bdab1bcf3c");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "duplicateId",
                table: "HotelBookings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a2e7fc4e-1499-42d9-a9e4-80a82b2378a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "9a5f784b-4320-4b52-ac82-60f34b14c67b");
        }
    }
}
