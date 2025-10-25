using _15PC2_ECommerce.Services.DashboardServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _15PC2_ECommerce.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var totalRevenue = await _dashboardService.GetTotalRevenueAsync();
            var topProduct = await _dashboardService.GetTopSellingProductAsync();
            var orderCount = await _dashboardService.GetOrdersLastThreeMonthsAsync();
            var topCustomer = await _dashboardService.GetTopCustomerAsync();
            var customerSegments = await _dashboardService.GetCustomerSegmentationAsync();
            var last10Orders = await _dashboardService.GetLast10OrdersAsync();
            var order10CountByCity = await _dashboardService.Get10OrderCountByCityAsync();
            var orderCountByCity = await _dashboardService.GetOrderCountByCityAsync();
            var orderStatsByCity = await _dashboardService.GetOrderStatsByCityAsync();

            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.TopProduct = topProduct;
            ViewBag.OrderCount = orderCount;
            ViewBag.TopCustomer = topCustomer;

            ViewBag.Order10CountByCity = order10CountByCity;
            ViewBag.CustomerSegmentation = customerSegments;
            ViewBag.Last10Orders = last10Orders;
            ViewBag.OrderCountByCity = orderCountByCity;
            ViewBag.OrderStatsByCity = orderStatsByCity;

            return View();
        }
    }
}
