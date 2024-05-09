using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehicule004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CoutReparations",
                table: "Vehicule",
                type: "decimal(7,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Indisponible",
                table: "Vehicule",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoutReparations",
                table: "Vehicule");

            migrationBuilder.DropColumn(
                name: "Indisponible",
                table: "Vehicule");
        }
    }
}
