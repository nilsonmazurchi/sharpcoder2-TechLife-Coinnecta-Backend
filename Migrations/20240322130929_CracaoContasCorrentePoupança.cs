using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CracaoContasCorrentePoupança : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Usuarios_ContaPoupanca_UsuarioId",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Usuarios_UsuarioId",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_Conta_ContaDestinoId",
                table: "Transacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_Conta_ContaOrigemId",
                table: "Transacaos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conta",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_ContaPoupanca_UsuarioId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "ContaPoupanca_UsuarioId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "TempoDeAberturaConta",
                table: "Conta");

            migrationBuilder.RenameTable(
                name: "Conta",
                newName: "ContaPoupancas");

            migrationBuilder.RenameColumn(
                name: "TaxaRendimento",
                table: "ContaPoupancas",
                newName: "Rendimento");

            migrationBuilder.RenameIndex(
                name: "IX_Conta_UsuarioId",
                table: "ContaPoupancas",
                newName: "IX_ContaPoupancas_UsuarioId");

            migrationBuilder.AlterColumn<int>(
                name: "ContaDestinoId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TipoConta",
                table: "ContaPoupancas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "StatusConta",
                table: "ContaPoupancas",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaPoupancas",
                table: "ContaPoupancas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContaCorrentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LimiteCredito = table.Column<double>(type: "REAL", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumeroConta = table.Column<string>(type: "TEXT", nullable: true),
                    TipoConta = table.Column<string>(type: "TEXT", nullable: true),
                    StatusConta = table.Column<string>(type: "TEXT", nullable: true),
                    Saldo = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCorrentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaCorrentes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContaCorrentes_UsuarioId",
                table: "ContaCorrentes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContaPoupancas_Usuarios_UsuarioId",
                table: "ContaPoupancas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaDestinoId",
                table: "Transacaos",
                column: "ContaDestinoId",
                principalTable: "ContaCorrentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaOrigemId",
                table: "Transacaos",
                column: "ContaOrigemId",
                principalTable: "ContaCorrentes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContaPoupancas_Usuarios_UsuarioId",
                table: "ContaPoupancas");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaDestinoId",
                table: "Transacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaOrigemId",
                table: "Transacaos");

            migrationBuilder.DropTable(
                name: "ContaCorrentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaPoupancas",
                table: "ContaPoupancas");

            migrationBuilder.RenameTable(
                name: "ContaPoupancas",
                newName: "Conta");

            migrationBuilder.RenameColumn(
                name: "Rendimento",
                table: "Conta",
                newName: "TaxaRendimento");

            migrationBuilder.RenameIndex(
                name: "IX_ContaPoupancas_UsuarioId",
                table: "Conta",
                newName: "IX_Conta_UsuarioId");

            migrationBuilder.AlterColumn<int>(
                name: "ContaDestinoId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "TipoConta",
                table: "Conta",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StatusConta",
                table: "Conta",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContaPoupanca_UsuarioId",
                table: "Conta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Conta",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TempoDeAberturaConta",
                table: "Conta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conta",
                table: "Conta",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Usuarios_UsuarioId",
                table: "Conta",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_Conta_ContaDestinoId",
                table: "Transacaos",
                column: "ContaDestinoId",
                principalTable: "Conta",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_Conta_ContaOrigemId",
                table: "Transacaos",
                column: "ContaOrigemId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
