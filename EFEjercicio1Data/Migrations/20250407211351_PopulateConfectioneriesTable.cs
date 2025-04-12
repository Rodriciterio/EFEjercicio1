using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFEjercicio1Data.Migrations
{
    /// <inheritdoc />
    public partial class PopulateConfectioneriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Confectioneries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    {1, "Cafe Martinez" },
                    {2, "Gran Cafe Tortoni" },
                    {3, "Las Violetas" },
                    {4, "La Giralda" },
                    {5, "Atalaya" },
                    {6, "Co-Pain" }
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Confectioneries",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6 });
        }
    }
}
