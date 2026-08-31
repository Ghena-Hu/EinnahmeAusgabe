using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EinnahmeAusgabe.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kategorien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Farbe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategorien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaktionen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bezeichnung = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Betrag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Erstellungsdatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    KategorieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaktionen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaktionen_kategorien_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "kategorien",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaktionen_KategorieId",
                table: "Transaktionen",
                column: "KategorieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaktionen");

            migrationBuilder.DropTable(
                name: "kategorien");
        }
    }
}
