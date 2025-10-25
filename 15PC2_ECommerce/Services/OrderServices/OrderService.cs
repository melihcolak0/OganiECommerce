using _15PC2_ECommerce.Context;
using _15PC2_ECommerce.DTOs.OrderDTOs;
using _15PC2_ECommerce.Entities;
using _15PC2_ECommerce.Services.LogServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _15PC2_ECommerce.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public OrderService(AppDbContext context, IMapper mapper, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrderDTO)
        {
            var order = _mapper.Map<Order>(createOrderDTO);

            order.OrderDate = createOrderDTO.OrderDate.Kind == DateTimeKind.Utc
              ? createOrderDTO.OrderDate
              : createOrderDTO.OrderDate.ToUniversalTime();

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Order", "CREATE", $"Sipariş oluşturuldu! (Id: {order.OrderId})");
        }

        public async Task<List<ResultOrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
        .Include(o => o.Customer)
        .Include(o => o.Product)
        .ToListAsync();

            var result = orders.Select(o => new ResultOrderDTO
            {
                OrderId = o.OrderId,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer?.Name,
                CustomerSurname = o.Customer?.Surname,
                ProductId = o.ProductId,
                ProductName = o.Product?.ProductName,
                Amount = o.Amount,
                TotalPrice = o.TotalPrice,
                OrderDate = DateTime.SpecifyKind(o.OrderDate, DateTimeKind.Utc).ToLocalTime()
            }).ToList();

            return result;
        }

        public async Task<GetOrderDTO> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
                return null;

            var result = _mapper.Map<GetOrderDTO>(order);
            result.CustomerName = order.Customer?.Name;
            result.CustomerSurname = order.Customer?.Surname;
            result.ProductName = order.Product?.ProductName;
            result.OrderDate = DateTime.SpecifyKind(order.OrderDate, DateTimeKind.Utc).ToLocalTime();

            return result;
        }

        public async Task RemoveOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Order", "DELETE", $"Sipariş silindi! (Id: {order.OrderId})");
        }

        public async Task UpdatOrderAsync(UpdateOrderDTO updateOrderDTO)
        {
            var order = await _context.Orders.FindAsync(updateOrderDTO.OrderId);

            if (order == null)
                return;

            _mapper.Map(updateOrderDTO, order);

            order.OrderDate = updateOrderDTO.OrderDate.Kind == DateTimeKind.Utc
                ? updateOrderDTO.OrderDate
                : updateOrderDTO.OrderDate.ToUniversalTime();

            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Order", "UPDATE", $"Sipariş güncellendi! (Id: {order.OrderId})");
        }
    }
}
