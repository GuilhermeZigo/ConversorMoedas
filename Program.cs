using System;
using System.Threading.Tasks;

namespace ConversorMoedas
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Conversor de Moedas ===\n");

            while (true)
            {
                Console.WriteLine("Escolha uma opção:");
                Console.WriteLine("1. Converter Moedas");
                Console.WriteLine("2. Sair");
                Console.Write("Opção: ");

                string opcao = Console.ReadLine();

                if (opcao == "2")
                {
                    Console.WriteLine("Saindo...");
                    break;
                }
                else if (opcao == "1")
                {
                    await ConverterMoedas();
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }
            }
        }

        static async Task ConverterMoedas()
        {
            // Opções de moedas
            string[] moedasDisponiveis = { "USD", "BRL", "EUR", "GBP", "JPY", "CAD", "AUD" };
            Console.WriteLine("Moedas disponíveis: " + string.Join(", ", moedasDisponiveis) + "\n");

            Console.Write("Moeda de origem (ex: USD): ");
            string moedaOrigem = Console.ReadLine()?.Trim().ToUpper();

            Console.Write("Moeda de destino (ex: BRL): ");
            string moedaDestino = Console.ReadLine()?.Trim().ToUpper();

            Console.Write("Valor a ser convertido: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal valor))
            {
                Console.WriteLine("Valor inválido.");
                return;
            }

            var resultado = await CurrencyConverter.ConverterAsync(moedaOrigem, moedaDestino, valor);

            if (resultado != null)
            {
                Console.WriteLine($"\n{valor:N2} {moedaOrigem} = {resultado.Value:N2} {moedaDestino}");
            }
            else
            {
                Console.WriteLine("\nErro ao realizar a conversão. Verifique os códigos das moedas e tente novamente.");
            }
        }
    }
}
