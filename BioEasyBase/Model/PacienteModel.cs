using System;

namespace BioEasyBase.Model
{
    public class PacienteModel
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public Decimal Altura { get; set; }

        public String Sexo { get; set; }

        public String Email { get; set; }
        
        public String Celular { get; set; }

        public String CPF { get; set; }

        public Guid IdEndereco { get; set; }

        public String EnderecoLinha { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime UltimaConsulta { get; set; }

        public String Comentario { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdEmpresa { get; set; }

        public String CEP { get; set; }

        public String Logradouro { get; set; }

        public String Numero { get; set; }

        public String Complemento { get; set; }

        public String Bairro { get; set; }

        public String Cidade { get; set; }

        public String UF { get; set; }
    }
}
