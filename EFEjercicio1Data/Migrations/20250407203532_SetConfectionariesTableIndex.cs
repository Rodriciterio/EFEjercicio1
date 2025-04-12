using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFEjercicio1Data.Migrations
{
    /// <inheritdoc />
    public partial class SetConfectionariesTableIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Confectioneries",
                newName: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Confectionaries_Name",
                table: "Confectioneries",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Confectionaries_Name",
                table: "Confectioneries");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Confectioneries",
                newName: "Nombre");
        }
    }
}
