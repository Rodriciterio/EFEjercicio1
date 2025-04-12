using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFEjercicio1Data.Migrations
{
    /// <inheritdoc />
    public partial class AnotherPopulationDrinksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "ConfectioneryId", "Name", "Size" },
                values: new object[,]
                {
                    { 8, 5, "Americano", "Mediano" },
                    { 9, 2, "Irish Coffe", "Mediano" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Drinks",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
