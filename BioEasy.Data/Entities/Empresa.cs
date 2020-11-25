using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BioEasy.Data.Entities
{
    public class Empresa : BaseEntity
    {
        [Required(ErrorMessage = "Insira o nome da empresa")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Display(Name = "Registro no Conselho")]
        public string CRN_CRM { get; set; }
        [Display(Name = "CPF/CNPJ")]
        public string CPF_CNPJ { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        [Display(Name = "Instagram")]
        public string Instagram { get; set; }
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
        [Display(Name = "Logo")]
        public byte[] Logo { get; set; }
        [Display(Name = "Balança")]
        public string Balanca { get; set; }
    }
}
