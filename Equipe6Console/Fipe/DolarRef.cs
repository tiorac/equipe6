using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6Console.Fipe
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class DolarRef
    {
        [FieldConverter(ConverterKind.Date, "dd/MM/yy")]
        public DateTime MesAno { get; set; }

        public double Last { get; set; }

        public double Opening { get; set; }

        public double Max { get; set; }

        public double Min { get; set; }
    }
}
