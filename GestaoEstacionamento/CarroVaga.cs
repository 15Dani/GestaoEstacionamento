using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace GestaoEstacionamento
{
    public class CarroVaga
    {
        [Index(0)]
        public string Placa { get; set; }
        [Index(1)]
        public string IdVaga { get; set; }
        [Index(2), DateTimeStyles(DateTimeStyles.AssumeLocal)]
        public DateTime HoraEntrada { get; set; }
        [Index(3), DateTimeStyles(DateTimeStyles.AssumeLocal)]
        public DateTime HoraSaida { get; set; }
        public TimeSpan Duração => HoraSaida - HoraEntrada;
    }
}
    

