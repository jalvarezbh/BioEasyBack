using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BioEasy.Data.Migrations
{
    public partial class update04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AguaCorporal",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoDireitoMassaAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoDireitoMassaMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoDireitoMassaNaoAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoDireitoNivelGordura",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoDireitoQualidadeMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoEsquerdoMassaAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoEsquerdoMassaMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoEsquerdoMassaNaoAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoEsquerdoNivelGordura",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BracoEsquerdoQualidadeMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacao",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRegistroBalanca",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "GorduraVisceral",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IdadeMetabolica",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "IngestaoCalorica",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MassaAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MassaCorporal",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MassaMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MassaNaoAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MassaOssea",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NivelGordura",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaDireitaMassaAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaDireitaMassaMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaDireitaMassaNaoAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaDireitaNivelGordura",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaDireitaQualidadeMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaEsquerdaMassaAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaEsquerdaMassaMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaEsquerdaMassaNaoAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaEsquerdaNivelGordura",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PernaEsquerdaQualidadeMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Peso",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "QualidadeMuscularTotal",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxaMetabolicaBasal",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TroncoMassaAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TroncoMassaMuscular",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TroncoMassaNaoAdiposa",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TroncoNivelGordura",
                table: "HistoricoPaciente",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AcucarNoSangue",
                table: "AnalisesLaboratoriais",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Colesterol",
                table: "AnalisesLaboratoriais",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataLancamento",
                table: "AnalisesLaboratoriais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "HDL",
                table: "AnalisesLaboratoriais",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LDL",
                table: "AnalisesLaboratoriais",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Triglicerideos",
                table: "AnalisesLaboratoriais",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "UltimaRefeicao",
                table: "AnalisesLaboratoriais",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AguaCorporal",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoDireitoMassaAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoDireitoMassaMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoDireitoMassaNaoAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoDireitoNivelGordura",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoDireitoQualidadeMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoEsquerdoMassaAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoEsquerdoMassaMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoEsquerdoMassaNaoAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoEsquerdoNivelGordura",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "BracoEsquerdoQualidadeMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "DataAtualizacao",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "DataRegistroBalanca",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "GorduraVisceral",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "IdadeMetabolica",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "IngestaoCalorica",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "MassaAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "MassaCorporal",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "MassaMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "MassaNaoAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "MassaOssea",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "NivelGordura",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaDireitaMassaAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaDireitaMassaMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaDireitaMassaNaoAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaDireitaNivelGordura",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaDireitaQualidadeMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaEsquerdaMassaAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaEsquerdaMassaMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaEsquerdaMassaNaoAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaEsquerdaNivelGordura",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "PernaEsquerdaQualidadeMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "QualidadeMuscularTotal",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "TaxaMetabolicaBasal",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "TroncoMassaAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "TroncoMassaMuscular",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "TroncoMassaNaoAdiposa",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "TroncoNivelGordura",
                table: "HistoricoPaciente");

            migrationBuilder.DropColumn(
                name: "AcucarNoSangue",
                table: "AnalisesLaboratoriais");

            migrationBuilder.DropColumn(
                name: "Colesterol",
                table: "AnalisesLaboratoriais");

            migrationBuilder.DropColumn(
                name: "DataLancamento",
                table: "AnalisesLaboratoriais");

            migrationBuilder.DropColumn(
                name: "HDL",
                table: "AnalisesLaboratoriais");

            migrationBuilder.DropColumn(
                name: "LDL",
                table: "AnalisesLaboratoriais");

            migrationBuilder.DropColumn(
                name: "Triglicerideos",
                table: "AnalisesLaboratoriais");

            migrationBuilder.DropColumn(
                name: "UltimaRefeicao",
                table: "AnalisesLaboratoriais");
        }
    }
}
