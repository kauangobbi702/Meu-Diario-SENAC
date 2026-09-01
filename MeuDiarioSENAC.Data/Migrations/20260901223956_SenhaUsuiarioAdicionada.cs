using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuDiarioSENAC.Data.Migrations
{
    /// <inheritdoc />
    public partial class SenhaUsuiarioAdicionada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "ID");
        }
    }
}
