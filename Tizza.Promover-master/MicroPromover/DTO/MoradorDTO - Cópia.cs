﻿namespace Tizza
{
    public class MoradorDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int CodigoResidencia { get; set; }

        public int CodigoCondominio { get; set; }

        public ResidenciaDTO Residencia { get; set; }
    }
}
