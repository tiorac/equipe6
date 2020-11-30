using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6Console.Fipe
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class PrecoPetroleo
    {
        public string Data { get; set; }

        public double Valor { get; set; }

        [FieldHidden]
        public DateTime DataFormatada { get; set; }

        [FieldHidden]
        public double Variacao { get; set; }
    }
}
