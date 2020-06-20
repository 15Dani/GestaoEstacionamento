using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace GestaoEstacionamento
{
    class Program
    {
        // Declaração das variáveis.
        private static List<Carro> carros { get; set; }
        private static List<Vaga> vagas { get; set; }
        private static List<UsoDeVaga> usoDeVagas { get; set; }

        private static void CarregarListas()
        {
            carros = new List<Carro>();
            vagas = new List<Vaga>();
            usoDeVagas = new List<UsoDeVaga>();

            using (var reader = new StreamReader(@"C:\Users\DANI\source\repos\AppGestaoEstacionamento\AppGestaoEstacionamento\Carros.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                carros.AddRange(csv.GetRecords<Carro>());
            }

            using (var reader = new StreamReader(@"C:\Users\DANI\source\repos\AppGestaoEstacionamento\AppGestaoEstacionamento\Vagas.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                vagas.AddRange(csv.GetRecords<Vaga>());
            }

            using (var reader = new StreamReader(@"C:\Users\DANI\source\repos\AppGestaoEstacionamento\AppGestaoEstacionamento\Uso.txt"))
            using (var csv = new CsvReader(reader, CultureInfo.CurrentCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.MissingFieldFound = null;
                usoDeVagas.AddRange(csv.GetRecords<UsoDeVaga>());
            }

            foreach (var vaga in vagas)
                Console.WriteLine($"{vaga.IdEstacionamento}");

            foreach (var carro in carros)
                Console.WriteLine(carro.Placa);

            foreach (var usoDeVaga in usoDeVagas)
                Console.WriteLine($"{usoDeVaga.Placa} usou a vaga {usoDeVaga.IdVaga} de {usoDeVaga.Entrada} até {usoDeVaga.Saida}(duração: {usoDeVaga.DuracaoDias} dias)");
        }

        private static void OpcaoConsultaPorDatas()
        {
            //Console.Clear();
            Console.WriteLine("Consulta por datas");

            Console.WriteLine("");
            Console.Write("Digite a data inicio: ");
            var dataInicio = Console.ReadLine();

            Console.WriteLine("");
            Console.Write("Digite a data fim: ");
            var dataFim = Console.ReadLine();

            // Converte as strings no formato DateTime para comparação.
            var dataIni = DateTime.Parse(dataInicio).Date;
            var dataFin = DateTime.Parse(dataFim).Date;

            // Filtra as vagas de dentro do intervalo.
            var vagasResult = usoDeVagas
                .Where(p => p.Entrada.Date >= dataIni && p.Saida <= dataFin)
                .Select(p => p)
                .ToList();

            //
            Console.Clear();

            // Retorna a mensagem caso não seja encontrada nenhuma vaga.
            if (!vagasResult.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não foram encontrados resultados para esta data...pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Exibindo uso de vagas encontrado");
            Console.WriteLine("");

            foreach (var vaga in vagasResult)
            {
                Console.WriteLine($"{vaga.Placa} com entrada {vaga.Entrada:dd/MM/yyyy HH:mm} e saida {vaga.Saida:dd/MM/yyyy HH:mm}");
            }

            Console.ReadKey();
        }

        private static void OpcaoConsultaPorVeiculo()
        {
            Console.Clear();
            Console.WriteLine("Consulta por veículo");
            Console.WriteLine("");
            Console.Write("Digite a placa do veículo: ");
            var placa = Console.ReadLine();

            var vagasResult = usoDeVagas
                .Where(p => p.Placa == placa)
                .Select(p => p)
                .ToList();

            Console.Clear();

            if (!vagasResult.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não foram encontrados resultados para estas datas...pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Exibindo uso de vagas encontrado");
            Console.WriteLine("");

            var valorHora = 15;
            var valorTotal = 0D;

            foreach (var vaga in vagasResult)
            {
                var diferenca = vaga.Saida - vaga.Entrada;
                var valor = diferenca.TotalHours * valorHora;
                valorTotal += valor;

                Console.WriteLine($"{vaga.Placa} estacionado em {vaga.Entrada:dd/MM/yyyy} por {diferenca.TotalHours} horas no valor de {valor}");
            }

            Console.WriteLine($"Valor total de {valorTotal}");
            Console.ReadKey();
        }

        private static void OpcaoConsultaPorData()
        {
            Console.Clear();
            Console.WriteLine("Consulta por data");
            Console.WriteLine("");
            Console.Write("Digite a data: ");
            var data = Console.ReadLine();
            var dataData = DateTime.Parse(data).Date;

            var vagasResult = usoDeVagas.Where(p => p.Entrada.Date == dataData).Select(p => p).ToList();

            Console.Clear();

            if (!vagasResult.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Não foram encontrados resultados para esta data...pressione qualquer tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Exibindo uso de vagas encontrado");
            Console.WriteLine("");

            foreach (var vaga in vagasResult)
            {
                Console.WriteLine($"{vaga.Placa} com entrada {vaga.Entrada:dd/MM/yyyy HH:mm} e saida {vaga.Saida:dd/MM/yyyy HH:mm}");
            }

            Console.ReadKey();
        }

        private static string Menu()
        {
            var validOptions = new[] { "1", "2", "3", "4" };
            var valid = false;
            var option = string.Empty;

            while (!valid)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Seja bem vindo a Gestão de Estacionamento");
                Console.WriteLine();
                Console.WriteLine("Selecione uma das opções abaixo:");
                Console.WriteLine(" 1 - Mostrar quem estacionou em uma determinada vaga ao longo de um período entre duas datas");
                Console.WriteLine(" 2 - Exibir, para um veículo, todas as vezes que ele estacionou, o valor de cada uso e o valor total pago");
                Console.WriteLine(" 3 - Mostrar, para uma data, todas as entradas e saídas de um estacionamento, em ordem cronológica");
                Console.WriteLine(" 4 - Sair");

                option = Console.ReadLine();

                valid = validOptions.Contains(option);
            }

            return option;
        }

        static void Main(string[] args)
        { 
            // Carrega os arquivos.
            CarregarListas();
            
            // Chama o menu e retorna a opção escolhida.
            switch (Menu())
            {
                case "1":
                    OpcaoConsultaPorDatas();
                    break;
                case "2":
                    OpcaoConsultaPorVeiculo();
                    break;
                case "3":
                    OpcaoConsultaPorData();
                    break;
            }
        }
    }
}