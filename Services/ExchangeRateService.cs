using System.Text.Json;

namespace PruebaNet.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRateResponse?> GetLatestRatesAsync()
        {
            try
            {
                // Frankfurter API es una API gratuita para tipos de cambio
                var response = await _httpClient.GetAsync("https://api.frankfurter.app/latest?from=USD");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<ExchangeRateResponse>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                // Manejo de errores simplificado
            }
            return null;
        }
    }

    public class ExchangeRateResponse
    {
        public double Amount { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
