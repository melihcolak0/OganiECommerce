using _15PC2_ECommerce.DTOs.CategoryDTOs;

namespace _15PC2_ECommerce.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDTO>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO);
        Task RemoveCategoryAsync(int id);
        Task<GetCategoryDTO> GetCategoryByIdAsync(int id);
    }
}
