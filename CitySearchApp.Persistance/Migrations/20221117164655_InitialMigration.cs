using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitySearchApp.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitiesCZ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Obec = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObecCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Okres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OkresCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Kraj = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KrajCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PSC = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitiesCZ", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitiesCZ");
        }
    }
}
