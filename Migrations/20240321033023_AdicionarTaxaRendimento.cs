using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarTaxaRendimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LimiteCredito",
                table: "Conta");

            migrationBuilder.AddColumn<int>(
                name: "ContaPoupanca_UsuarioId",
                table: "Conta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TaxaRendimento",
                table: "Conta",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Conta_ContaPoupanca_UsuarioId",
                table: "Conta",
                column: "ContaPoupanca_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Usuarios_ContaPoupanca_UsuarioId",
                table: "Conta",
                column: "ContaPoupanca_UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Usuarios_ContaPoupanca_UsuarioId",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_ContaPoupanca_UsuarioId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "ContaPoupanca_UsuarioId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "TaxaRendimento",
                table: "Conta");

            migrationBuilder.AddColumn<double>(
                name: "LimiteCredito",
                table: "Conta",
                type: "REAL",
                nullable: true);
        }
    }
}
