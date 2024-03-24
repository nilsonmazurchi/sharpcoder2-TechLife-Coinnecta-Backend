using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class CracaoContasCorrentePoupançaRendimento01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaDestinoId",
                table: "Transacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaOrigemId",
                table: "Transacaos");

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ContaDestinoId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaDestinoId",
                table: "Transacaos",
                column: "ContaDestinoId",
                principalTable: "ContaCorrentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaOrigemId",
                table: "Transacaos",
                column: "ContaOrigemId",
                principalTable: "ContaCorrentes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaDestinoId",
                table: "Transacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_ContaCorrentes_ContaOrigemId",
                table: "Transacaos");

            migrationBuilder.AlterColumn<int>(
                name: "ContaOrigemId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContaDestinoId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

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
    }
}
