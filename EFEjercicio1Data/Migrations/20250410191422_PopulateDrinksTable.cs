using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFEjercicio1Data.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDrinksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Drinks (Name, Size, ConfectioneryId) VALUES ('Lagrima', 'Grande', 1)");
            migrationBuilder.Sql("INSERT INTO Drinks (Name, Size, ConfectioneryId) VALUES ('Cafe Cortado', 'Mediano', 2)");
            migrationBuilder.Sql("INSERT INTO Drinks (Name, Size, ConfectioneryId) VALUES ('Cafe Doble', 'Grande', 1)");
            migrationBuilder.Sql("INSERT INTO Drinks (Name, Size, ConfectioneryId) VALUES ('Cafe Macchiato', 'Grande', 7)");
            migrationBuilder.Sql("INSERT INTO Drinks (Name, Size, ConfectioneryId) VALUES ('Cappuccino', 'Extra Grande', 7)");
            migrationBuilder.Sql("INSERT INTO Drinks (Name, Size, ConfectioneryId) VALUES ('Frappuccino', 'Chico', 4)");
            migrationBuilder.Sql("INSERT INTO Drinks (Name, Size, ConfectioneryId) VALUES ('Flat White', 'Grande', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Drinks WHERE Id IN (1, 2, 3, 4, 5, 6, 7)");
        }
    }
}
