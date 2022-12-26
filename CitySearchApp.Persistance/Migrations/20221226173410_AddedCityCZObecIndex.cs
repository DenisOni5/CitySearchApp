using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitySearchApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddedCityCZObecIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CitiesCZ_Obec",
                table: "CitiesCZ",
                column: "Obec");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CitiesCZ_Obec",
                table: "CitiesCZ");
        }
    }
}
