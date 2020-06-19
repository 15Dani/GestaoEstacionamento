using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEstacionamento
{
    public class Vaga
    {
        [Index(0)]
        public string IdEstacionamento { get; set; }
        [Index(1)]
        public string IdVaga { get; set; }


    }

}

