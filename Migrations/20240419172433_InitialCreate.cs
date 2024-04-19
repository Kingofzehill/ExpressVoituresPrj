using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpressVoitures.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibelleMarque = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marque", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reparation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibelleReparation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modele",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibelleModele = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MarqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modele", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modele_Marque_MarqueId",
                        column: x => x.MarqueId,
                        principalTable: "Marque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Finition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibelleFinition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModeleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Finition_Modele_ModeleId",
                        column: x => x.ModeleId,
                        principalTable: "Modele",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 17, nullable: false),
                    Statut = table.Column<int>(type: "int", maxLength: 30, nullable: false),
                    Information = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Photo = table.Column<byte[]>(type: "varbinary(4000)", maxLength: 4000, nullable: true),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrixAchat = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    AnneeVehicule = table.Column<int>(type: "int", nullable: false),
                    MisEnVente = table.Column<bool>(type: "bit", nullable: false),
                    PrixDeVente = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    DateMisEnVente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Vendu = table.Column<bool>(type: "bit", nullable: false),
                    DateVente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicule_Finition_FinitionId",
                        column: x => x.FinitionId,
                        principalTable: "Finition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReparationVehicule",
                columns: table => new
                {
                    ReparationsId = table.Column<int>(type: "int", nullable: false),
                    VehiculesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReparationVehicule", x => new { x.ReparationsId, x.VehiculesId });
                    table.ForeignKey(
                        name: "FK_ReparationVehicule_Reparation_ReparationsId",
                        column: x => x.ReparationsId,
                        principalTable: "Reparation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReparationVehicule_Vehicule_VehiculesId",
                        column: x => x.VehiculesId,
                        principalTable: "Vehicule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finition_ModeleId",
                table: "Finition",
                column: "ModeleId");

            migrationBuilder.CreateIndex(
                name: "IX_Modele_MarqueId",
                table: "Modele",
                column: "MarqueId");

            migrationBuilder.CreateIndex(
                name: "IX_ReparationVehicule_VehiculesId",
                table: "ReparationVehicule",
                column: "VehiculesId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicule_FinitionId",
                table: "Vehicule",
                column: "FinitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReparationVehicule");

            migrationBuilder.DropTable(
                name: "Reparation");

            migrationBuilder.DropTable(
                name: "Vehicule");

            migrationBuilder.DropTable(
                name: "Finition");

            migrationBuilder.DropTable(
                name: "Modele");

            migrationBuilder.DropTable(
                name: "Marque");
        }
    }
}
