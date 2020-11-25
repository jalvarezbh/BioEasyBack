using System;

namespace BioEasyBase.Model
{
    public class LaboratorialModel
    {
        public Guid Id { get; set; }

        public String Nome { get; set; }        

        public Decimal Colesterol { get; set; }

        public Decimal LDL { get; set; }

        public Decimal HDL { get; set; }

        public Decimal Trigliceres { get; set; }

        public Decimal Acucar { get; set; }

        public DateTime DataExame { get; set; }

        public DateTime DataAvaliacao { get; set; }

        public short UltimaRefeicao { get; set; }

        public Guid IdPaciente { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdEmpresa { get; set; }

    }
}
