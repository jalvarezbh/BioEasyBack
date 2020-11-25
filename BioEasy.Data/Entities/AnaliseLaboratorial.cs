using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BioEasy.Data.Entities
{
    public class AnaliseLaboratorial : BaseEntity
    {
        [Display(Name = "Colesterol")]
        public decimal Colesterol { get; set; }

        [Display(Name = "LDL")]
        public decimal LDL { get; set; }

        [Display(Name = "HDL")]
        public decimal HDL { get; set; }

        [Display(Name = "Triglicerídeos")]
        public decimal Triglicerideos { get; set; }

        [Display(Name = "Açúcar no Sangue")]
        public decimal AcucarNoSangue { get; set; }

        [Display(Name = "Última Refeição")]
        public DateTime UltimaRefeicao { get; set; }

        [Display(Name = "Data do Lançamento")]
        public DateTime DataLancamento { get; set; }

        [Required(ErrorMessage = "Selecione o paciente ao qual essa análise está associada")]
        [ForeignKey("Paciente")]
        [Display(Name = "Paciente")]
        public int PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public Paciente Paciente { get; set; }
    }
}
