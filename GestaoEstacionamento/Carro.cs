using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GestaoEstacionamento
{
    public class Carro
    {
        [Index(0)]
        public string Placa { get; set; }

    }
}

   
    
