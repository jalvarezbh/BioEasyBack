using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BioEasy.Data.Entities;

namespace BioEasy.ViewModels
{
    public class Prevencao
    {
        [Display(Name = "Nome do Paciente")]
        public string Nome { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Altura")]
        public decimal Altura { get; set; }
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        public HistoricoPaciente HistoricoPaciente { get; set; }

        public AnaliseLaboratorial AnaliseLaboratorial { get; set; }
    }
}
