using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateContas01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContaCorrente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaxaRendimento = table.Column<double>(type: "REAL", nullable: false),
                    Rendimento = table.Column<double>(type: "REAL", nullable: false),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    TempoDeAberturaConta = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    NumeroConta = table.Column<string>(type: "TEXT", nullable: true),
                    TipoConta = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusConta = table.Column<int>(type: "INTEGER", nullable: false),
                    Balanco = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaCorrente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContaCorrente_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContaPoupanca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LimiteCredito = table.Column<double>(type: "REAL", nullable: false),
                    NumeroConta = table.Column<string>(type: "TEXT", nullable: true),
                    TipoConta = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusConta = table.Column<int>(type: "INTEGER", nullable: false),
                    Balanco = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaPoupanca", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContaCorrente_UsuarioId",
                table: "ContaCorrente",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContaCorrente");

            migrationBuilder.DropTable(
                name: "ContaPoupanca");
        }
    }
}
