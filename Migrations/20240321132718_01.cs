using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_Conta_ContaId",
                table: "Transacaos");

            migrationBuilder.RenameColumn(
                name: "ContaId",
                table: "Transacaos",
                newName: "ContaOrigemId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacaos_ContaId",
                table: "Transacaos",
                newName: "IX_Transacaos_ContaOrigemId");

            migrationBuilder.AddColumn<int>(
                name: "ContaDestinoId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Valor",
                table: "Transacaos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Transacaos_ContaDestinoId",
                table: "Transacaos",
                column: "ContaDestinoId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_Conta_ContaDestinoId",
                table: "Transacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_Conta_ContaOrigemId",
                table: "Transacaos");

            migrationBuilder.DropIndex(
                name: "IX_Transacaos_ContaDestinoId",
                table: "Transacaos");

            migrationBuilder.DropColumn(
                name: "ContaDestinoId",
                table: "Transacaos");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Transacaos");

            migrationBuilder.RenameColumn(
                name: "ContaOrigemId",
                table: "Transacaos",
                newName: "ContaId");

            migrationBuilder.RenameIndex(
                name: "IX_Transacaos_ContaOrigemId",
                table: "Transacaos",
                newName: "IX_Transacaos_ContaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_Conta_ContaId",
                table: "Transacaos",
                column: "ContaId",
                principalTable: "Conta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
