using _15PC2_ECommerce.DTOs.DashboardDTOs;
using _15PC2_ECommerce.DTOs.OrderDTOs;

namespace _15PC2_ECommerce.Services.DashboardServices
{
    public interface IDashboardService
    {
        Task<decimal> GetTotalRevenueAsync();
        Task<string> GetTopSellingProductAsync();
        Task<int> GetOrdersLastThreeMonthsAsync();
        Task<string> GetTopCustomerAsync();

        Task<List<CityOrderCountDTO>> Get10OrderCountByCityAsync();
        Task<List<CustomerSegmentDto>> GetCustomerSegmentationAsync();
        Task<List<ResultOrderDTO>> GetLast10OrdersAsync();
        Task<List<CityOrderCountDTO>> GetOrderCountByCityAsync();
        Task<List<CityOrderStatsDTO>> GetOrderStatsByCityAsync();
    }
}
