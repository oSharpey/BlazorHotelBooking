using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class domoreidthingsssss4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "duplicateId",
                table: "HotelBookings",
                newName: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "288e07a7-8e67-4b58-9c8f-d0555be594bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5c756df1-38dd-4ff4-8667-af7307928570");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "HotelBookings",
                newName: "duplicateId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "a218429b-3be8-408a-a7c5-d7163e97593c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a6ea050c-3975-4135-8937-fb9b860d5380");
        }
    }
}
