using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class coinnecta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacaos_Conta_ContaDestinoId",
                table: "Transacaos");

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
                name: "FK_Transacaos_Conta_ContaDestinoId",
                table: "Transacaos",
                column: "ContaDestinoId",
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

            migrationBuilder.AlterColumn<int>(
                name: "ContaDestinoId",
                table: "Transacaos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacaos_Conta_ContaDestinoId",
                table: "Transacaos",
                column: "ContaDestinoId",
                principalTable: "Conta",
                principalColumn: "Id");
        }
    }
}
