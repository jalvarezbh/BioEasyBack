using System;

namespace BioEasyBase.Model
{
    public class UsuarioCompletoModel
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }

        public String Email { get; set; }

        public String CPF { get; set; }

        public String Celular { get; set; }

        public String Documento { get; set; }
        
        public String Especialidade { get; set; }

        public String Senha { get; set; }

        public bool Ativo { get; set; }

        public bool Administrador { get; set; }

        public DateTime DataLimite { get; set; }

        public DateTime UltimoLogin { get; set; }

        public Guid IdAutonomo { get; set; }

        public Guid IdEmpresa { get; set; }

        public String EmpresaSolicitada { get; set; }

        public String NomeComercial { get; set; }

        public String CNPJ { get; set; }

        public String Telefone { get; set; }

        public String EmailComercial { get; set; }

        public String Instagram { get; set; }

        public String Logo { get; set; }

        public String Balanca { get; set; }

        public Guid IdEndereco { get; set; }
       
        public String CEP { get; set; }

        public String Logradouro { get; set; }

        public String Numero { get; set; }

        public String Complemento { get; set; }

        public String Bairro { get; set; }

        public String Cidade { get; set; }

        public String UF { get; set; }
    }
}
