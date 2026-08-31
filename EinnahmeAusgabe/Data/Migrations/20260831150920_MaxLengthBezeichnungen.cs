using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EinnahmeAusgabe.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaxLengthBezeichnungen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaktionen_kategorien_KategorieId",
                table: "Transaktionen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_kategorien",
                table: "kategorien");

            migrationBuilder.RenameTable(
                name: "kategorien",
                newName: "Kategorien");

            migrationBuilder.AlterColumn<string>(
                name: "Bezeichnung",
                table: "Transaktionen",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Kategorien",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kategorien",
                table: "Kategorien",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaktionen_Kategorien_KategorieId",
                table: "Transaktionen",
                column: "KategorieId",
                principalTable: "Kategorien",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaktionen_Kategorien_KategorieId",
                table: "Transaktionen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kategorien",
                table: "Kategorien");

            migrationBuilder.RenameTable(
                name: "Kategorien",
                newName: "kategorien");

            migrationBuilder.AlterColumn<string>(
                name: "Bezeichnung",
                table: "Transaktionen",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "kategorien",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_kategorien",
                table: "kategorien",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaktionen_kategorien_KategorieId",
                table: "Transaktionen",
                column: "KategorieId",
                principalTable: "kategorien",
                principalColumn: "Id");
        }
    }
}
