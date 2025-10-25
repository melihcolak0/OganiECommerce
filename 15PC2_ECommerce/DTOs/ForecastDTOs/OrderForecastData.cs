namespace _15PC2_ECommerce.DTOs.ForecastDTOs
{
    public class OrderForecastData
    {
        public string City { get; set; }
        public float MonthNumber { get; set; }
        public float OrderCount { get; set; }
    }

    public class OrderForecastPrediction
    {
        public float[] ForecastedOrders { get; set; }
    }
}
