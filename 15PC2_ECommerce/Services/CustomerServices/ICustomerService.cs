using _15PC2_ECommerce.DTOs.CategoryDTOs;
using _15PC2_ECommerce.DTOs.CustomerDTOs;

namespace _15PC2_ECommerce.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<List<ResultCustomerDTO>> GetAllCustomersAsync();
        Task CreateCustomerAsync(CreateCustomerDTO createCustomerDTO);
        Task UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO);
        Task RemoveCustomerAsync(int id);
        Task<GetCustomerDTO> GetCustomerByIdAsync(int id);
    }
}
