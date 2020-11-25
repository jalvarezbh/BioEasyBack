using System;

namespace BioEasyBase.Model
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }

        public String Email { get; set; }

        public String CPF { get; set; }
                
        public String Celular { get; set; }

        public String Documento { get; set; }

        public String Especialidade { get; set; }

        public String Senha { get; set; }

        public String Balanca { get; set; }

        public bool Ativo { get; set; }

        public bool Administrador { get; set; }

        public DateTime DataLimite { get; set; }

        public DateTime UltimoLogin { get; set; }

        public Guid IdAutonomo { get; set; }

        public Guid IdEmpresa { get; set; }

        public String EmpresaSolicitada { get; set; }

        public Guid IdSimulado { get; set; }

        public void PreencherUsuario(UsuarioCompletoModel registro)
        {
            Id = registro.Id;
            Nome = registro.Nome;
            Email = registro.Email;
            CPF = registro.CPF;
            Celular = registro.Celular;
            Documento = registro.Documento;
            Especialidade = registro.Especialidade;
            Ativo = registro.Ativo;
            Administrador = registro.Administrador;
            DataLimite = registro.DataLimite;
            IdAutonomo = registro.IdAutonomo;
            IdEmpresa = registro.IdEmpresa;
            EmpresaSolicitada = registro.EmpresaSolicitada;
        }
    }
}
