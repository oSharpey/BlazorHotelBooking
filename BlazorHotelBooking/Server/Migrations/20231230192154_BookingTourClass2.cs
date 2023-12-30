using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class BookingTourClass2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TourBookins",
                table: "TourBookins");

            migrationBuilder.RenameTable(
                name: "TourBookins",
                newName: "TourBookings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourBookings",
                table: "TourBookings",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "34d39ab6-937d-467b-ad8d-2cbf29f69f87");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f0607d7b-526c-437f-b1a4-3871ac3f9ac2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TourBookings",
                table: "TourBookings");

            migrationBuilder.RenameTable(
                name: "TourBookings",
                newName: "TourBookins");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourBookins",
                table: "TourBookins",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "10709ebd-38a6-4bad-97c9-c37eba0f4ec1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "72158d74-18ea-40e1-8168-b751d48d357d");
        }
    }
}
