using System;

namespace BioEasyBase.Model
{
    public class RelatorioEnvioModel
    {
        public Guid IdPaciente { get; set; }
                
        public String Tipo { get; set; }

        public String Email { get; set; }
    }
}
