using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class cancelledhotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "paidInfull",
                table: "HotelBookings",
                newName: "PaidInfull");

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "HotelBookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f5ea8602-462f-42fa-8b29-a1c9402a1a78");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "1a438254-7794-4de2-950f-5ed9d79393f7");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "HotelBookings");

            migrationBuilder.RenameColumn(
                name: "PaidInfull",
                table: "HotelBookings",
                newName: "paidInfull");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "556405b6-f604-4a05-bfdb-9575dab9112d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8699aeb5-b1d3-4d99-8744-72ceb47a7d74");
        }
    }
}
