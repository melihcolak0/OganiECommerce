using _15PC2_ECommerce.DTOs.ProductDTOs;

namespace _15PC2_ECommerce.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDTO>> GetAllProductsAsync();
        Task CreateProductAsync(CreateProductDTO createProductDTO);
        Task UpdatProductAsync(UpdateProductDTO updateProductDTO);
        Task RemoveProductAsync(int id);
        Task<GetProductDTO> GetProductByIdAsync(int id);
        Task<List<ResultProductDTO>> GetPopularProductsAsync();
        Task<List<ResultProductDTO>> GetAllProductsOrderedAsync();
    }
}
