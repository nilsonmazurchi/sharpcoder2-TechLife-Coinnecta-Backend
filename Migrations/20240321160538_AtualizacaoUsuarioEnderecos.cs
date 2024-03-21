using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sharpcoder2_TechLife_Coinnecta_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoUsuarioEnderecos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusUsuario",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusUsuario",
                table: "Usuarios");
        }
    }
}
