using System.Net.Http.Json;

namespace HousePriceApi.Services
{
    public class ModelPredictionService : IModelPredictionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ModelPredictionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> GetPredictionFromPythonAsync(float totalArea, int bedrooms, float latitude, float longitude)
        {
            var mlApiUrl = _configuration["PythonMLApiUrl"] ?? "http://localhost:5000/predict";
            
            var payload = new { totalArea, bedrooms, latitude, longitude };
            
            var response = await _httpClient.PostAsJsonAsync(mlApiUrl, payload);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Failed to communicate with the Machine Learning microservice.");

            var result = await response.Content.ReadFromJsonAsync<PythonPredictionResponse>();
            return (decimal)(result?.PredictedPrice ?? 0);
        }
    }

    public class PythonPredictionResponse
    {
        public float PredictedPrice { get; set; }
    }
}