using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6Console.Fipe
{
    [DelimitedRecord("\t"), IgnoreFirst(1)]
    public class FipeCarroMesAnoRef
    {
        [FieldOrder(1), FieldCaption("Modelo")]
        public string Modelo { get; set; }

        [FieldOrder(2), FieldCaption("Marca")]
        public string Marca { get; set; }

        [FieldOrder(3), FieldCaption("Ano")]
        public string Ano { get; set; }

        [FieldOrder(4), FieldCaption("Combustivel")]
        public string Combustivel { get; set; }

        [FieldOrder(5), FieldCaption("AnoMesReferencia")]
        public string AnoMesReferencia { get; set; }

        [FieldOrder(6), FieldCaption("Valor")]
        public double Valor { get; set; }

        [FieldOrder(7), FieldCaption("Variacao")]
        public double Variacao { get; set; }

        [FieldOrder(8), FieldCaption("Dolar")]
        public double Dolar { get; set; }

        [FieldOrder(9), FieldCaption("VariacaoDolar")]
        public double VariacaoDolar { get; set; }

        [FieldOrder(10), FieldCaption("IGPM")]
        public double IGPM { get; set; }

        [FieldOrder(11), FieldCaption("VariacaoIGPM")]
        public double VariacaoIGPM { get; set; }

        [FieldOrder(12), FieldCaption("Petroleo")]
        public double Petroleo { get; set; }

        [FieldOrder(13), FieldCaption("VariacaoPetroleo")]
        public double VariacaoPetroleo { get; set; }
    }
}
