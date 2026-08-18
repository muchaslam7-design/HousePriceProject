namespace HousePriceApi.Services
{
    public interface IModelPredictionService
    {
        Task<decimal> GetPredictionFromPythonAsync(float totalArea, int bedrooms, float latitude, float longitude);
    }
}