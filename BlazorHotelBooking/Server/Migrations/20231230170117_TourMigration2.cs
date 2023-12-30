using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class TourMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DurationInDays = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "58ea16a6-58ac-47b5-bf2d-792b9cb3d25a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "54a778f1-64bd-4963-b89e-cc5d970576dc");

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "Cost", "Description", "DurationInDays", "Name" },
                values: new object[,]
                {
                    { 1, 1200m, "Dive into charming villages, rolling hills, and iconic castles in this 6-day escape to authentic Britain", 6, "Real Britain" },
                    { 2, 2000m, "Journey through 16 days of cityscapes, dramatic coasts, and Celtic charm. Uncover the best of Britain and Ireland.", 16, "Britain and Ireland Explorer" },
                    { 3, 2900m, "Indulge in 12 days of luxury. Explore stately homes, savor Michelin stars, and discover hidden gems of Britain's finest", 12, "Best of Britain" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "00efb88a-129b-4cbd-92b4-3523b060a745");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e107e00d-73f9-445c-b74b-b04f3718f015");
        }
    }
}
