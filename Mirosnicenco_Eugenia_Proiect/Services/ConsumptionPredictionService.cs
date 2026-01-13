using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Mirosnicenco_Eugenia_Proiect.Models;

namespace Mirosnicenco_Eugenia_Proiect.Services
{
    public class ConsumptionPredictionService : IConsumptionPredictionService
    {
        private readonly HttpClient _httpClient;

        public ConsumptionPredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double> PredictConsumptionAsync(ConsumptionInput input)
        {
            var response = await _httpClient.PostAsJsonAsync("/predict", input);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ConsumptionApiResponse>();

            return result?.Score ?? 0;
        }

        private class ConsumptionApiResponse
        {
            public double Score { get; set; }
        }
    }
}
