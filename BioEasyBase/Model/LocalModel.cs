using System;

namespace BioEasyBase.Model
{
    public class LocalModel
    {
        public Guid Id { get; set; }

        public TipoEndereco Tipo { get; set; }

        public String CEP { get; set; }

        public String Endereco { get; set; }

        public String Numero { get; set; }

        public String Complemento { get; set; }

        public String Bairro { get; set; }

        public String Cidade { get; set; }

        public String UF { get; set; }

        public String Pais { get; set; }

    }

    public enum TipoEndereco
    {
        Paciente = 1,
        Usuario = 2,
        Empresa = 3,
    }
}
