﻿// <auto-generated />
using System;
using BioEasy.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BioEasy.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200810133101_update09")]
    partial class update09
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BioEasy.Data.Entities.AnaliseLaboratorial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AcucarNoSangue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Colesterol")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataLancamento")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("HDL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("LDL")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<decimal>("Triglicerideos")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UltimaRefeicao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("AnalisesLaboratoriais");
                });

            modelBuilder.Entity("BioEasy.Data.Entities.Configuracao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Porta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SMTP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Configuracao");
                });

            modelBuilder.Entity("BioEasy.Data.Entities.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Balanca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CPF_CNPJ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CRN_CRM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instagram")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("BioEasy.Data.Entities.HistoricoPaciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AguaCorporal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoDireitoMassaAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoDireitoMassaMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoDireitoMassaNaoAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoDireitoNivelGordura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoDireitoQualidadeMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoEsquerdoMassaAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoEsquerdoMassaMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoEsquerdoMassaNaoAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoEsquerdoNivelGordura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BracoEsquerdoQualidadeMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataRegistroBalanca")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GorduraVisceral")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdadeMetabolica")
                        .HasColumnType("int");

                    b.Property<decimal>("IngestaoCalorica")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MassaAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MassaCorporal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MassaMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MassaNaoAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MassaOssea")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("NivelGordura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.Property<decimal>("PernaDireitaMassaAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaDireitaMassaMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaDireitaMassaNaoAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaDireitaNivelGordura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaDireitaQualidadeMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaEsquerdaMassaAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaEsquerdaMassaMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaEsquerdaMassaNaoAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaEsquerdaNivelGordura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PernaEsquerdaQualidadeMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Peso")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("QualidadeMuscularTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TaxaMetabolicaBasal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TroncoMassaAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TroncoMassaMuscular")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TroncoMassaNaoAdiposa")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TroncoNivelGordura")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PacienteId");

                    b.ToTable("HistoricoPaciente");
                });

            modelBuilder.Entity("BioEasy.Data.Entities.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Altura")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Comentarios")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("BioEasy.Data.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Administrador")
                        .HasColumnType("bit");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoginDataAte")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PrimeiroAcesso")
                        .HasColumnType("bit");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("BioEasy.Data.Entities.AnaliseLaboratorial", b =>
                {
                    b.HasOne("BioEasy.Data.Entities.Paciente", "Paciente")
                        .WithMany()
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BioEasy.Data.Entities.HistoricoPaciente", b =>
                {
                    b.HasOne("BioEasy.Data.Entities.Paciente", "Paciente")
                        .WithMany("HistoricoPacientes")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BioEasy.Data.Entities.Paciente", b =>
                {
                    b.HasOne("BioEasy.Data.Entities.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BioEasy.Data.Entities.Usuario", b =>
                {
                    b.HasOne("BioEasy.Data.Entities.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
