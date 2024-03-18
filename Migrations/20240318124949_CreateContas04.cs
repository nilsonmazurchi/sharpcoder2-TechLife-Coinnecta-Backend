using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateContas04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balanco",
                table: "ContaCorrentes");

            migrationBuilder.RenameColumn(
                name: "Balanco",
                table: "ContaPoupancas",
                newName: "Saldo");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "ContaCorrentes",
                newName: "Saldo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Saldo",
                table: "ContaPoupancas",
                newName: "Balanco");

            migrationBuilder.RenameColumn(
                name: "Saldo",
                table: "ContaCorrentes",
                newName: "Valor");

            migrationBuilder.AddColumn<double>(
                name: "Balanco",
                table: "ContaCorrentes",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
