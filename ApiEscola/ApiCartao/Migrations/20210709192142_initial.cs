using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiEscola.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TelefoneEscola",
                table: "Escola",
                newName: "telefoneEscola");

            migrationBuilder.RenameColumn(
                name: "NomeEscola",
                table: "Escola",
                newName: "nomeEscola");

            migrationBuilder.RenameColumn(
                name: "EnderecoEscola",
                table: "Escola",
                newName: "enderecoEscola");

            migrationBuilder.RenameColumn(
                name: "EscolaId",
                table: "Escola",
                newName: "escolaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "telefoneEscola",
                table: "Escola",
                newName: "TelefoneEscola");

            migrationBuilder.RenameColumn(
                name: "nomeEscola",
                table: "Escola",
                newName: "NomeEscola");

            migrationBuilder.RenameColumn(
                name: "enderecoEscola",
                table: "Escola",
                newName: "EnderecoEscola");

            migrationBuilder.RenameColumn(
                name: "escolaId",
                table: "Escola",
                newName: "EscolaId");
        }
    }
}
