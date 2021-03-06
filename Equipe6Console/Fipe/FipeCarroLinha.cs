﻿using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Equipe6.Fipe
{
    [DelimitedRecord("\t")]
    public class FipeCarroLinha
    {
        public string Modelo { get; set; }

        public string Marca { get; set; }

        public string Ano { get; set; }

        public string Combustivel { get; set; }

        public string Valor { get; set; }

        public string Bla { get; set; }
    }
}
