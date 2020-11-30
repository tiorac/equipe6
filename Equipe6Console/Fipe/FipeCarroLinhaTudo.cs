using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Fipe
{
    [DelimitedRecord("\t")]
    public class FipeCarroLinhaTudo
    {
        
        public string Modelo { get; set; }

        public string Marca { get; set; }

        public string Ano { get; set; }

        public string Combustivel { get; set; }

        public double Valor { get; set; }

        public string MesReferencia { get; set; }

        public int AnoReferencia { get; set; }
    }
}
