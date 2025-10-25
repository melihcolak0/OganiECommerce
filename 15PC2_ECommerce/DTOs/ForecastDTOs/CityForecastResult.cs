namespace _15PC2_ECommerce.DTOs.ForecastDTOs
{
    public class CityForecastResult
    {
        public string City { get; set; }
        public float[] HistoricalOrders { get; set; }
        public float[] ForecastedOrders { get; set; }
    }
}
