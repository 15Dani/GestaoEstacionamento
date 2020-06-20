using CsvHelper.Configuration.Attributes;
using System;

namespace GestaoEstacionamento
{
    public class UsoDeVaga
    {
        [Index(0)]
        public string Placa { get; set; }
        [Index(1)]
        public string IdVaga { get; set; }
        [Index(2)]
        public DateTime Entrada { get; set; }
        [Index(3)]
        public DateTime Saida { get; set; }

        public int DuracaoDias => Saida.Date.Day - Entrada.Date.Day;
    }
}