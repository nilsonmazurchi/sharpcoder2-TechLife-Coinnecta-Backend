using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class transacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaCorrentes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaPoupancas",
                table: "ContaPoupancas");

            migrationBuilder.RenameTable(
                name: "ContaPoupancas",
                newName: "Conta");

            migrationBuilder.AlterColumn<double>(
                name: "LimiteCredito",
                table: "Conta",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

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

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Conta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conta",
                table: "Conta",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Transacaos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataHoraTrasacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DescricaoTrasacao = table.Column<string>(type: "TEXT", nullable: true),
                    TipoTransacao = table.Column<int>(type: "INTEGER", nullable: false),
                    ContaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacaos_Conta_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Conta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conta_UsuarioId",
                table: "Conta",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacaos_ContaId",
                table: "Transacaos",
                column: "ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Usuarios_UsuarioId",
                table: "Conta",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Usuarios_UsuarioId",
                table: "Conta");

            migrationBuilder.DropTable(
                name: "Transacaos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conta",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_UsuarioId",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "TempoDeAberturaConta",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Conta");

            migrationBuilder.RenameTable(
                name: "Conta",
                newName: "ContaPoupancas");

            migrationBuilder.AlterColumn<double>(
                name: "LimiteCredito",
                table: "ContaPoupancas",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

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
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumeroConta = table.Column<string>(type: "TEXT", nullable: true),
                    Rendimento = table.Column<double>(type: "REAL", nullable: false),
                    Saldo = table.Column<double>(type: "REAL", nullable: false),
                    StatusConta = table.Column<int>(type: "INTEGER", nullable: false),
                    TaxaRendimento = table.Column<double>(type: "REAL", nullable: false),
                    TempoDeAberturaConta = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TipoConta = table.Column<int>(type: "INTEGER", nullable: false)
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
        }
    }
}
