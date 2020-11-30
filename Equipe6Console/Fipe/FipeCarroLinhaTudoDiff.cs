using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Fipe
{
    [DelimitedRecord("\t")]
    public class FipeCarroLinhaTudoDiff : FipeCarroLinhaTudo
    {
        public double Variacao { get; set; }

        public double VariacaoDolar { get; set; }

        public double VariacaoIGPM { get; set; }
    }
}
