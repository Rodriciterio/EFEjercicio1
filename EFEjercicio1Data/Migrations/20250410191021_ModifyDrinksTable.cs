using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFEjercicio1Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyDrinksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drink_Confectioneries_ConfectioneryId",
                table: "Drink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drink",
                table: "Drink");

            migrationBuilder.RenameTable(
                name: "Drink",
                newName: "Drinks");

            migrationBuilder.RenameIndex(
                name: "IX_Drink_ConfectioneryId",
                table: "Drinks",
                newName: "IX_Drinks_ConfectioneryId");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Drinks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drinks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_DrinkId",
                table: "Drinks",
                columns: new[] { "Name", "Size" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Confectioneries_ConfectioneryId",
                table: "Drinks",
                column: "ConfectioneryId",
                principalTable: "Confectioneries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Confectioneries_ConfectioneryId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drinks",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_DrinkId",
                table: "Drinks");

            migrationBuilder.RenameTable(
                name: "Drinks",
                newName: "Drink");

            migrationBuilder.RenameIndex(
                name: "IX_Drinks_ConfectioneryId",
                table: "Drink",
                newName: "IX_Drink_ConfectioneryId");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Drink",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Drink",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drink",
                table: "Drink",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drink_Confectioneries_ConfectioneryId",
                table: "Drink",
                column: "ConfectioneryId",
                principalTable: "Confectioneries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
