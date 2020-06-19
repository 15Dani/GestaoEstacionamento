using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace GestaoEstacionamento
{
    class Program
    {
        static void Main(string[] args)
        {
            var carros = new List<Carro>();
            var vagas = new List<Vaga>();
            var carrosEmVagas = new List<CarroVaga>();

            int opcao = 0;
            // Arquivo dados = new Arquivo();
            //teste

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
                carrosEmVagas.AddRange(csv.GetRecords<CarroVaga>());
            }

            foreach (var vaga in vagas)
                Console.WriteLine($"{vaga.IdEstacionamento}");

            foreach (var carro in carros)
                Console.WriteLine(carro.Placa);

            foreach (var carroEmVaga in carrosEmVagas)
                Console.WriteLine($"{carroEmVaga.Placa} usou a vaga {carroEmVaga.IdVaga} de {carroEmVaga.HoraEntrada} até {carroEmVaga.HoraSaida}(duração: {carroEmVaga.Duração.TotalMinutes} minutos)");


            while (opcao != 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Seja bem vindo a Gestão de Estacionamento");
                Console.WriteLine();
                Console.WriteLine("Selecione uma das opções abaixo:" +
                   "\n 1 - Mostrar quem estacionou em uma determinada vaga ao longo de um período entre duas datas " +
                   "\n 2 - Exibir, para um veículo, todas as vezes que ele estacionou, o valor de cada uso e o valor total pago" +
                   "\n 3 - Mostrar, para uma data, todas as entradas e saídas de um estacionamento, em ordem cronológica" +
                   "\n 4 - Sair" +
                   "\n Digite a opção selecionada:");
                opcao = int.Parse(Console.ReadLine());
            }
            if (opcao == 1)
            {
                Console.Clear();

                Console.WriteLine("Qual a vaga que deseja pesquisar?");
                string vaga = Console.ReadLine();

                //Vaga vagas = v
                //Vaga = dados.LeitorEstacionada(null, vagaArvore);

                ////VagaArvore vagaArvore = dados.LeitorVagas();
                ////vagaArvore = dados.LeitorEstacionada(null, vagaArvore);

                Console.WriteLine("Digite data inicial");
                string intervaloIni = Console.ReadLine();

                Console.WriteLine("Digite data final");
                string intervaloFim = Console.ReadLine();

                //vagaArvore.PrintarEstaciondas(vaga, intervaloIni, intervaloFim);

                Console.WriteLine("Digite algo para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}


    


        
    

