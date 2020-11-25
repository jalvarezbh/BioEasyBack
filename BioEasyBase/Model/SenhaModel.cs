using System;

namespace BioEasyBase.Model
{
    public class SenhaModel
    {
        public Guid Id { get; set; }

        public String SenhaAtual { get; set; }

        public String SenhaNova { get; set; }

        public String SenhaRepetir { get; set; }
    }
}
