using _15PC2_ECommerce.Context;
using _15PC2_ECommerce.DTOs.DashboardDTOs;
using _15PC2_ECommerce.DTOs.OrderDTOs;
using Microsoft.EntityFrameworkCore;

namespace _15PC2_ECommerce.Services.DashboardServices
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerSegmentDto>> GetCustomerSegmentationAsync()
        {
            var customerOrders = await _context.Customers
                .Select(c => new
                {
                    CustomerId = c.CustomerId,                   
                    TotalOrders = c.Orders.Count()
                
                })
                .ToListAsync();

            var segmentation = customerOrders
                .GroupBy(co =>
                    co.TotalOrders switch
                    {
                        var count when count >= 210 => "Altın",
                        var count when count >= 180 && count <= 209 => "Gümüş",
                        var count when count <= 179 => "Bronz",
                        _ => "Tanımsız"
                    })
                .Select(g => new CustomerSegmentDto
                {
                    SegmentName = g.Key,       
                    CustomerCount = g.Count()   
                })
                .OrderByDescending(dto => dto.CustomerCount)
                .ToList();

            return segmentation;
        }

        public Task<List<ResultOrderDTO>> GetLast10OrdersAsync()
        {
            var orders = _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(10)
                .Select(o => new ResultOrderDTO
                {
                    OrderId = o.OrderId,
                    CustomerName = o.Customer.Name,
                    ProductName = o.Product.ProductName,
                    Amount = o.Amount,
                    TotalPrice = o.TotalPrice,
                    OrderDate = o.OrderDate,
                    CustomerId = o.CustomerId,
                    CustomerSurname = o.Customer.Surname,
                    ProductId = o.ProductId                    
                })
                .ToListAsync();

            return orders;
        }

        public async Task<List<CityOrderCountDTO>> Get10OrderCountByCityAsync()
        {
            var orderCounts = await _context.Orders
            .GroupBy(o => o.Customer.City)
            .Select(g => new CityOrderCountDTO
            {
                City = g.Key,
                OrderCount = g.Count()
            })
            .OrderByDescending(x => x.OrderCount)
            .Take(10)
            .ToListAsync();

            return orderCounts;
        }

        public async Task<int> GetOrdersLastThreeMonthsAsync()
        {
            var startDate = new DateTime(2025, 10, 1).ToUniversalTime();
            var endDate = new DateTime(2026, 1, 1).ToUniversalTime();

            var orderCount = await _context.Orders
                .Where(o => o.OrderDate >= startDate && o.OrderDate < endDate)
                .CountAsync();

            return orderCount;
        }

        public async Task<string> GetTopSellingProductAsync()
        {
            var topProduct = await _context.Orders
                                    .Include(o => o.Product)
                                    .GroupBy(o => o.Product.ProductName)
                                    .Select(g => new
                                    {
                                        ProductName = g.Key,
                                        TotalSold = g.Sum(x => x.Amount)
                                    })
                                    .OrderByDescending(x => x.TotalSold)
                                    .FirstOrDefaultAsync();

            return topProduct?.ProductName ?? "Henüz satış yok";
        }

        async Task<string> IDashboardService.GetTopCustomerAsync()
        {
            var topCustomer = await _context.Orders
                                                .Include(o => o.Customer)
                                                .GroupBy(o => new { o.CustomerId, o.Customer.Name, o.Customer.Surname, o.Customer.City })
                                                .Select(g => new
                                                {
                                                    FullInfo = g.Key.Name + " " + g.Key.Surname + " (" + g.Key.City + ")",
                                                    TotalProducts = g.Sum(x => (int)x.Amount)
                                                })
                                                .OrderByDescending(x => x.TotalProducts)
                                                .FirstOrDefaultAsync();

            return topCustomer?.FullInfo ?? "Henüz müşteri yok";
        }

        async Task<decimal> IDashboardService.GetTotalRevenueAsync()
        {
            var totalRevenue = await _context.Orders.SumAsync(o => o.TotalPrice);
            return totalRevenue;
        }

        public async Task<List<CityOrderCountDTO>> GetOrderCountByCityAsync()
        {
            var result = await _context.Orders
                .Include(o => o.Customer)
                .GroupBy(o => o.Customer.City)
                .Select(g => new CityOrderCountDTO
                {
                    City = g.Key,
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.OrderCount)
                .ToListAsync();

            return result;
        }

        public async Task<List<CityOrderStatsDTO>> GetOrderStatsByCityAsync()
        {
            var result = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .GroupBy(o => o.Customer.City)
                .Select(g => new CityOrderStatsDTO
                {
                    City = g.Key,
                    OrderCount = g.Count(),
                    AverageOrderAmount = g.Average(o => o.TotalPrice),
                    TopCategory = g
                        .GroupBy(o => o.Product.Category.CategoryName)
                        .OrderByDescending(cg => cg.Count())
                        .Select(cg => cg.Key)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return result;
        }
    }
}
