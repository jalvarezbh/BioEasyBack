using System;

namespace BioEasyBase.Model
{
    public class EnderecoModel
    {
        public Guid Id { get; set; }

        public String CEP { get; set; }

        public String Logradouro { get; set; }

        public String Numero { get; set; }

        public String Complemento { get; set; }

        public String Bairro { get; set; }

        public String Cidade { get; set; }

        public String UF { get; set; }
    }
}
