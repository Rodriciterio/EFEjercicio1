using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFEjercicio1Data.Migrations
{
    /// <inheritdoc />
    public partial class SetNoCascadeDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Confectioneries_ConfectioneryId",
                table: "Drinks");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Confectioneries_ConfectioneryId",
                table: "Drinks",
                column: "ConfectioneryId",
                principalTable: "Confectioneries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Confectioneries_ConfectioneryId",
                table: "Drinks");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Confectioneries_ConfectioneryId",
                table: "Drinks",
                column: "ConfectioneryId",
                principalTable: "Confectioneries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
