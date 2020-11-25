using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BioEasy.Data.Entities
{
    public class Paciente : BaseEntity
    {
        [Required(ErrorMessage = "Insira o Nome do paciente")]
        [Display(Name = "Nome do Paciente")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Insira a data de nascimento do paciente")]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataNascimento { get; set; }
        [Display(Name = "Altura(cm)")]
        public decimal Altura { get; set; }
        [Required(ErrorMessage = "Selecione o sexo do paciente")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        [Display(Name = "Comentários")]
        public string Comentarios { get; set; }

        [Required(ErrorMessage = "Selecione a empresa a qual esse usuário está associado")]
        [ForeignKey("Empresa")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public ICollection<HistoricoPaciente> HistoricoPacientes { get; set; }
    }
}
