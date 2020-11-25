using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEasy.Data.Migrations
{
    public partial class update02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Paciente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comentarios",
                table: "Paciente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Paciente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Paciente",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Paciente",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Paciente",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Comentarios",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Paciente");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Paciente");
        }
    }
}
