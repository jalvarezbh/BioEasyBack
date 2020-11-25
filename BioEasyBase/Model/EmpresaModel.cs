using System;

namespace BioEasyBase.Model
{
    public class EmpresaModel
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }

        public String CNPJ { get; set; }

        public String Telefone { get; set; }

        public String Email { get; set; }

        public String Instagram { get; set; }

        public String Logo { get; set; }

        public String Balanca { get; set; }

        public Guid IdEndereco { get; set; }

        public Boolean Ativo { get; set; }

        public String CEP { get; set; }

        public String Logradouro { get; set; }

        public String Numero { get; set; }

        public String Complemento { get; set; }

        public String Bairro { get; set; }

        public String Cidade { get; set; }

        public String UF { get; set; }

        public void PreencherEmpresa(UsuarioCompletoModel registro)
        {
            Id = registro.IdEmpresa;
            Nome = registro.NomeComercial;
            CNPJ = registro.CNPJ;
            Telefone = registro.Telefone;
            Email = registro.EmailComercial;
            Instagram = registro.Instagram;
            Logo = registro.Logo;
            Balanca = registro.Balanca;
            IdEndereco = registro.IdEndereco;
            CEP = registro.CEP;
            Logradouro = registro.Logradouro;
            Numero = registro.Numero;
            Complemento = registro.Complemento;
            Bairro = registro.Bairro;
            Cidade = registro.Cidade;
            UF = registro.UF;
        }
    }
}
