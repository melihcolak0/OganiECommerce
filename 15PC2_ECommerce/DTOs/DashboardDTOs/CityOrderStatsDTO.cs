namespace _15PC2_ECommerce.DTOs.DashboardDTOs
{
    public class CityOrderStatsDTO
    {
        public string City { get; set; }
        public int OrderCount { get; set; }
        public decimal AverageOrderAmount { get; set; }
        public string TopCategory { get; set; }
    }
}
