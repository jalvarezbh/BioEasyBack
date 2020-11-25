using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BioEasy.Data.Entities
{
    public class Configuracao : BaseEntity
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Porta")]
        public string Porta { get; set; }

        [Display(Name = "SMTP")]
        public string SMTP { get; set; }

        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
