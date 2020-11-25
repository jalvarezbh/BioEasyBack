using System;

namespace BioEasyBase.Model
{
    public class SistemaModel
    {
        public Guid Id { get; set; }

        public String Email { get; set; }

        public String Senha { get; set; }

        public String SenhaRepetir { get; set; }

        public String Responsavel { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public Guid IdUsuario { get; set; }

        public bool Ativo { get; set; }

    }
}
