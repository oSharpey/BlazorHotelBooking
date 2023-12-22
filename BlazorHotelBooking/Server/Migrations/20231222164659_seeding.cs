using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "DBPrice", "Description", "FamPrice", "Name", "SBPrice", "Spaces" },
                values: new object[,]
                {
                    { 1, 775m, "Experience luxury in the heart of the city, with sophisticated rooms and a stone's throw from London's major attractions and shopping districts.", 950m, "Hilton London Hotel", 375m, 20 },
                    { 2, 500m, "Indulge in elegance and comfort at this centrally located hotel, featuring top-notch amenities and easy access to London's historical landmarks", 900m, "London Marriott Hotel", 300m, 20 },
                    { 3, 120m, " Enjoy affordable comfort with stunning seafront views, ideally situated for exploring Brighton’s vibrant beach and pier attractions.", 150m, "Travelodge Brighton Seafront", 80m, 20 },
                    { 4, 400m, "A charming, budget-friendly hotel on Brighton’s seafront, offering cozy accommodations with easy access to the city's lively nightlife and cultural sites", 520m, "Kings Hotel Brighton", 180m, 20 },
                    { 5, 400m, "Modern and stylish, this hotel provides a comfortable base to discover Brighton, conveniently close to the beach and the buzzing city center.", 520m, "Leonardo Hotel Brighton", 180m, 20 },
                    { 6, 100m, "Nestled in the Scottish Highlands, this inn offers a serene getaway with scenic views, perfect for outdoor enthusiasts and nature lovers", 155m, "Nevis Bank Inn, Fort William", 90m, 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
