namespace HousePriceApi.Models
{
    public class PredictionRecord
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public float TotalArea { get; set; }
        public int Bedrooms { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public decimal PredictedPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}