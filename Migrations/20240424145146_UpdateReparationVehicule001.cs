using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReparationVehicule001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CoutReparation",
                table: "ReparationVehicule",
                type: "decimal(7,2)",
                precision: 7,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoutReparation",
                table: "ReparationVehicule");
        }
    }
}
