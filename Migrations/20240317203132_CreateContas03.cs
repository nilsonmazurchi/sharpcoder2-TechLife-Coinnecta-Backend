using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateContas03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaCorrente_Usuarios_UsuarioId",
                table: "ContaCorrente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaPoupanca",
                table: "ContaPoupanca");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaCorrente",
                table: "ContaCorrente");

            migrationBuilder.RenameTable(
                name: "ContaPoupanca",
                newName: "ContaPoupancas");

            migrationBuilder.RenameTable(
                name: "ContaCorrente",
                newName: "ContaCorrentes");

            migrationBuilder.RenameIndex(
                name: "IX_ContaCorrente_UsuarioId",
                table: "ContaCorrentes",
                newName: "IX_ContaCorrentes_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaPoupancas",
                table: "ContaPoupancas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaCorrentes",
                table: "ContaCorrentes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaCorrentes_Usuarios_UsuarioId",
                table: "ContaCorrentes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaCorrentes_Usuarios_UsuarioId",
                table: "ContaCorrentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaPoupancas",
                table: "ContaPoupancas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaCorrentes",
                table: "ContaCorrentes");

            migrationBuilder.RenameTable(
                name: "ContaPoupancas",
                newName: "ContaPoupanca");

            migrationBuilder.RenameTable(
                name: "ContaCorrentes",
                newName: "ContaCorrente");

            migrationBuilder.RenameIndex(
                name: "IX_ContaCorrentes_UsuarioId",
                table: "ContaCorrente",
                newName: "IX_ContaCorrente_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaPoupanca",
                table: "ContaPoupanca",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaCorrente",
                table: "ContaCorrente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaCorrente_Usuarios_UsuarioId",
                table: "ContaCorrente",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
