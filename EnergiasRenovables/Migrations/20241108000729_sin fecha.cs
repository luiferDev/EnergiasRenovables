using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergiasRenovables.Migrations
{
    /// <inheritdoc />
    public partial class sinfecha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnergiaRequerida = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NivelCovertura = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Poblacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PlantaProduccionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEnergias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEnergias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantaProduccions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapacidadInstalada = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Eficiencia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantaProduccions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantaProduccions_Paises_Id",
                        column: x => x.Id,
                        principalTable: "Paises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergiasRenovables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoEnergiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergiasRenovables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergiasRenovables_PlantaProduccions_Id",
                        column: x => x.Id,
                        principalTable: "PlantaProduccions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnergiasRenovables_TipoEnergias_TipoEnergiaId",
                        column: x => x.TipoEnergiaId,
                        principalTable: "TipoEnergias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Biomasa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Origen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContenidoEnergetico = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetodoConversion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biomasa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biomasa_EnergiasRenovables_Id",
                        column: x => x.Id,
                        principalTable: "EnergiasRenovables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergiaEolicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    NumeroTurbinas = table.Column<int>(type: "int", nullable: false),
                    VelocidadViento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlturaTurbinas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiametroTurbina = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergiaEolicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergiaEolicas_EnergiasRenovables_Id",
                        column: x => x.Id,
                        principalTable: "EnergiasRenovables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergiaGeotermicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Caudal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroPozos = table.Column<int>(type: "int", nullable: false),
                    TemperaturaFluidos = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergiaGeotermicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergiaGeotermicas_EnergiasRenovables_Id",
                        column: x => x.Id,
                        principalTable: "EnergiasRenovables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergiaHidroelectricas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Salto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Caudal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumeroTurbinas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergiaHidroelectricas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergiaHidroelectricas_EnergiasRenovables_Id",
                        column: x => x.Id,
                        principalTable: "EnergiasRenovables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnergiaSolars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RadiacionSolar = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AreaPaneles = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnguloInclinacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EficienciaPaneles = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnergiaSolars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnergiaSolars_EnergiasRenovables_Id",
                        column: x => x.Id,
                        principalTable: "EnergiasRenovables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnergiasRenovables_TipoEnergiaId",
                table: "EnergiasRenovables",
                column: "TipoEnergiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biomasa");

            migrationBuilder.DropTable(
                name: "EnergiaEolicas");

            migrationBuilder.DropTable(
                name: "EnergiaGeotermicas");

            migrationBuilder.DropTable(
                name: "EnergiaHidroelectricas");

            migrationBuilder.DropTable(
                name: "EnergiaSolars");

            migrationBuilder.DropTable(
                name: "EnergiasRenovables");

            migrationBuilder.DropTable(
                name: "PlantaProduccions");

            migrationBuilder.DropTable(
                name: "TipoEnergias");

            migrationBuilder.DropTable(
                name: "Paises");
        }
    }
}
