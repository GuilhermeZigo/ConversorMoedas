using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConversorMoedas
{
    public static class CurrencyConverter
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<decimal?> ConverterAsync(string de, string para, decimal valor)
        {
            try
            {
                string url = $"https://api.exchangerate-api.com/v4/latest/{de}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                    return null;
                }

                string json = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                // Verifica se a propriedade "rates" existe
                if (root.TryGetProperty("rates", out JsonElement rates) &&
                    rates.TryGetProperty(para, out JsonElement rateElem))
                {
                    // A taxa pode ser um número, então vamos converter diretamente
                    if (rateElem.ValueKind == JsonValueKind.Number)
                    {
                        decimal rate = rateElem.GetDecimal(); // Obtendo o valor como decimal
                        // Arredondando o resultado da conversão para 2 casas decimais
                        return Math.Round(rate * valor, 2);
                    }
                    else
                    {
                        Console.WriteLine($"A taxa de câmbio para '{para}' não é um número.");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine($"Código da moeda de destino '{para}' não encontrado.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return null;
            }
        }
    }
}
