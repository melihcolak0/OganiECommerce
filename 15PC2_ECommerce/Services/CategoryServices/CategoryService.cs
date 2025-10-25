using _15PC2_ECommerce.Context;
using _15PC2_ECommerce.DTOs.CategoryDTOs;
using _15PC2_ECommerce.Entities;
using _15PC2_ECommerce.Services.LogServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _15PC2_ECommerce.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public CategoryService(AppDbContext context, IMapper mapper, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task CreateCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            var category = _mapper.Map<Category>(createCategoryDTO);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Category", "CREATE", $"Kategori oluşturuldu! (Id: {category.CategoryId})");
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<ResultCategoryDTO>>(categories);
        }

        public async Task<GetCategoryDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return _mapper.Map<GetCategoryDTO>(category);
        }

        public async Task RemoveCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Category", "DELETE", $"Kategori silindi! (Id: {category.CategoryId})");
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDTO updateCategoryDTO)
        {
            var category = await _context.Categories.FindAsync(updateCategoryDTO.CategoryId);

            if (category == null)
                return;

            _mapper.Map(updateCategoryDTO, category);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Category", "UPDATE", $"Kategori güncellendi! (Id: {category.CategoryId})");
        }
    }
}
