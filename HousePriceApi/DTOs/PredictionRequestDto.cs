namespace HousePriceApi.DTOs
{
    public class PredictionRequestDto
    {
        public float TotalArea { get; set; }
        public int Bedrooms { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}