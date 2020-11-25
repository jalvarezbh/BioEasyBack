using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEasy.Data.Migrations
{
    public partial class update05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Empresa_EmpresaId",
                table: "Paciente");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Paciente",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Empresa_EmpresaId",
                table: "Paciente",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paciente_Empresa_EmpresaId",
                table: "Paciente");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Paciente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Paciente_Empresa_EmpresaId",
                table: "Paciente",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
