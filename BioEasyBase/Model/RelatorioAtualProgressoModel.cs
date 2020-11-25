﻿using System;

namespace BioEasyBase.Model
{
    public class RelatorioAtualProgressoModel
    {
        public Guid Id { get; set; }

        public Decimal NivelGorduraTotal { get; set; }

        public Decimal NivelGorduraPD { get; set; }

        public Decimal NivelGorduraPE { get; set; }

        public Decimal NivelGorduraBD { get; set; }

        public Decimal NivelGorduraBE { get; set; }

        public Decimal NivelGorduraT { get; set; }

        public Decimal MassaAdiposaTotal { get; set; }

        public Decimal MassaAdiposaPD { get; set; }

        public Decimal MassaAdiposaPE { get; set; }

        public Decimal MassaAdiposaBD { get; set; }

        public Decimal MassaAdiposaBE { get; set; }

        public Decimal MassaAdiposaT { get; set; }

        public Decimal MassaNAdiposaTotal { get; set; }

        public Decimal MassaNAdiposaPD { get; set; }

        public Decimal MassaNAdiposaPE { get; set; }

        public Decimal MassaNAdiposaBD { get; set; }

        public Decimal MassaNAdiposaBE { get; set; }

        public Decimal MassaNAdiposaT { get; set; }

        public Decimal MassaMuscularTotal { get; set; }

        public Decimal MassaMuscularPD { get; set; }

        public Decimal MassaMuscularPE { get; set; }

        public Decimal MassaMuscularBD { get; set; }

        public Decimal MassaMuscularBE { get; set; }

        public Decimal MassaMuscularT { get; set; }

        public Decimal QualidadeMuscularTotal { get; set; }

        public Decimal QualidadeMuscularPD { get; set; }

        public Decimal QualidadeMuscularPE { get; set; }

        public Decimal QualidadeMuscularBD { get; set; }

        public Decimal QualidadeMuscularBE { get; set; }

        public Decimal PhysiqueRating { get; set; }

        public DateTime DataAvaliacao { get; set; }

        public Guid IdPaciente { get; set; }

        public Guid IdUsuario { get; set; }

        public Guid IdEmpresa { get; set; }
    }
}
