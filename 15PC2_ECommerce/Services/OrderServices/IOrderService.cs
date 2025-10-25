

using _15PC2_ECommerce.DTOs.OrderDTOs;

namespace _15PC2_ECommerce.Services.OrderServices
{
    public interface IOrderService
    {
        Task<List<ResultOrderDTO>> GetAllOrdersAsync();
        Task CreateOrderAsync(CreateOrderDTO createOrderDTO);
        Task UpdatOrderAsync(UpdateOrderDTO updateOrderDTO);
        Task RemoveOrderAsync(int id);
        Task<GetOrderDTO> GetOrderByIdAsync(int id);
    }
}
