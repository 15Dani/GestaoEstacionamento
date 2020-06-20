using CsvHelper.Configuration.Attributes;
using System;

namespace GestaoEstacionamento
{
    public class Carro
    {
        [Index(0)]
        public string Placa { get; set; }

    }
}