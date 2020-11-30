using FileHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Equipe6Console.Fipe
{
    [IgnoreFirst]
    [DelimitedRecord(";")]
    public class IGPMRef
    {
        public string MesAnoRef { get; set; }

        public string VariacaoRef { get; set; }

        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }

        internal DateTime MesAno
        {
            get
            {
                return DateTime.ParseExact(MesAnoRef, "MMMM/yyyy", CultureInfo.CurrentCulture);
            }
        }

        internal double Variacao
        {
            get
            {
                return Double.Parse(VariacaoRef, NumberStyles.Float, CultureInfo.CurrentCulture);
            }
        }
    }
}
