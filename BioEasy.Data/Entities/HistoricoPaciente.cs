using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BioEasy.Data.Entities
{
    public class HistoricoPaciente : BaseEntity
    {
        [Display(Name = "Peso")]
        public decimal Peso { get; set; }

        [Display(Name = "Nível de Gordura(%)")]
        public decimal NivelGordura { get; set; }

        [Display(Name = "Água Corporal/TAC(%)")]
        public decimal AguaCorporal { get; set; }

        [Display(Name = "Gordura Visceral(Nível)")]
        public decimal GorduraVisceral { get; set; }

        [Display(Name = "Massa Muscular/PMM(KG)")]
        public decimal MassaMuscular { get; set; }

        [Display(Name = "Massa Óssea(KG)")]
        public decimal MassaOssea { get; set; }

        [Display(Name = "Idade Metabólica")]
        public int IdadeMetabolica { get; set; }

        [Display(Name = "Massa Adiposa(KG)")]
        public decimal MassaAdiposa { get; set; }

        [Display(Name = "Massa não Adiposa/NMA(KG)")]
        public decimal MassaNaoAdiposa { get; set; }

        [Display(Name = "Ingestão Calorica(kcal)")]
        public decimal IngestaoCalorica { get; set; }

        [Display(Name = "Taxa Metabólica Basal(kcal)")]
        public decimal TaxaMetabolicaBasal { get; set; }

        [Display(Name = "Massa Corporal")]
        public decimal MassaCorporal { get; set; }

        [Display(Name = "Qualidade Muscular Total")]
        public decimal QualidadeMuscularTotal { get; set; }

        [Display(Name = "Nível de Gordura da Perna Direita(%)")]
        public decimal PernaDireitaNivelGordura { get; set; }

        [Display(Name = "Massa Adiposa da Perna Direita(KG)")]
        public decimal PernaDireitaMassaAdiposa { get; set; }

        [Display(Name = "Massa não Adiposa da Perna Direita/NMA(KG)")]
        public decimal PernaDireitaMassaNaoAdiposa { get; set; }

        [Display(Name = "Massa Muscular da Perna Direita/PMM(KG)")]
        public decimal PernaDireitaMassaMuscular { get; set; }

        [Display(Name = "Qualidade Muscular da Perna Direita")]
        public decimal PernaDireitaQualidadeMuscular { get; set; }

        [Display(Name = "Nível de Gordura da Perna Esquerda(%)")]
        public decimal PernaEsquerdaNivelGordura { get; set; }

        [Display(Name = "Massa Adiposa da Perna Esquerda(KG)")]
        public decimal PernaEsquerdaMassaAdiposa { get; set; }

        [Display(Name = "Massa não Adiposa da Perna Esquerda/NMA(KG)")]
        public decimal PernaEsquerdaMassaNaoAdiposa { get; set; }

        [Display(Name = "Massa Muscular da Perna Esquerda/PMM(KG)")]
        public decimal PernaEsquerdaMassaMuscular { get; set; }

        [Display(Name = "Qualidade Muscular da Perna Esquerda")]
        public decimal PernaEsquerdaQualidadeMuscular { get; set; }

        [Display(Name = "Nível de Gordura do Braço Direito(%)")]
        public decimal BracoDireitoNivelGordura { get; set; }

        [Display(Name = "Massa Adiposa do Braço Direito(KG)")]
        public decimal BracoDireitoMassaAdiposa { get; set; }

        [Display(Name = "Massa não Adiposa do Braço Direito/NMA(KG)")]
        public decimal BracoDireitoMassaNaoAdiposa { get; set; }

        [Display(Name = "Massa Muscular do Braço Direito/PMM(KG)")]
        public decimal BracoDireitoMassaMuscular { get; set; }

        [Display(Name = "Qualidade Muscular do Braço Direito")]
        public decimal BracoDireitoQualidadeMuscular { get; set; }

        [Display(Name = "Nível de Gordura do Braço Esquerdo(%)")]
        public decimal BracoEsquerdoNivelGordura { get; set; }

        [Display(Name = "Massa Adiposa do Braço Esquerdo(KG)")]
        public decimal BracoEsquerdoMassaAdiposa { get; set; }

        [Display(Name = "Massa não Adiposa do Braço Esquerdo/NMA(KG)")]
        public decimal BracoEsquerdoMassaNaoAdiposa { get; set; }

        [Display(Name = "Massa Muscular do Braço Esquerdo/PMM(KG)")]
        public decimal BracoEsquerdoMassaMuscular { get; set; }

        [Display(Name = "Qualidade Muscular do Braço Esquerdo")]
        public decimal BracoEsquerdoQualidadeMuscular { get; set; }

        [Display(Name = "Nível de Gordura do Tronco(%)")]
        public decimal TroncoNivelGordura { get; set; }

        [Display(Name = "Massa Adiposa do Tronco(KG)")]
        public decimal TroncoMassaAdiposa { get; set; }

        [Display(Name = "Massa não Adiposa do Tronco/NMA(KG)")]
        public decimal TroncoMassaNaoAdiposa { get; set; }

        [Display(Name = "Massa Muscular do Tronco/PMM(KG)")]
        public decimal TroncoMassaMuscular { get; set; }

        [Display(Name = "Data")]
        public DateTime DataRegistroBalanca { get; set; }

        [Display(Name = "Data de Atualização")]
        public DateTime DataAtualizacao { get; set; }

        [Required(ErrorMessage = "Selecione o paciente ao qual esse histórico está associado")]
        [ForeignKey("Paciente")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public Paciente Paciente { get; set; }
    }
}
