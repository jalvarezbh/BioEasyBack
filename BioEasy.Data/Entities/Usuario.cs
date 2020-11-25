using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BioEasy.Data.Entities
{
    public class Usuario : BaseEntity
    {
        [Required(ErrorMessage = "Insira o nome do usuário")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Insira a senha temporária do usuário")]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Insira o email do usuário(Necessário para alterar a senha)")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
        [Display(Name = "Primeiro Acesso")]
        public bool PrimeiroAcesso { get; set; }
        [Display(Name = "Administrador")]
        public bool Administrador { get; set; }
        [Required(ErrorMessage = "Selecione a empresa a qual esse usuário está associado")]
        [ForeignKey("Empresa")]
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        [Display(Name = "Empresa")]
        public Empresa Empresa { get; set; }
        [Display(Name = "Login válido até")]
        public DateTime LoginDataAte { get; set; }
    }
}
