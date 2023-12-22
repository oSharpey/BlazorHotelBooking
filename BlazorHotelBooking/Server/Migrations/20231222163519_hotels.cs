using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorHotelBooking.Server.Migrations
{
    /// <inheritdoc />
    public partial class hotels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SBPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DBPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FamPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Spaces = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
