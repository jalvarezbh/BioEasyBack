using System;

namespace BioEasyBase.Model
{
    public class ConfiguracaoModel
    {
        public Guid Id { get; set; }

        public bool PacienteAluno { get; set; }

        public bool PacienteEmail { get; set; }

        public bool PacienteCelular { get; set; }

        public bool PacienteCPF { get; set; }

        public bool PacienteEndCompleto { get; set; }

        public bool PacienteEndLinha { get; set; }

        public bool PacienteComentario { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public Guid IdUsuario { get; set; }
    }
}
