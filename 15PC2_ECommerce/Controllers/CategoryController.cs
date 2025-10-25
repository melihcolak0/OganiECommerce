using _15PC2_ECommerce.DTOs.CategoryDTOs;
using _15PC2_ECommerce.Entities;
using _15PC2_ECommerce.Services.CategoryServices;
using _15PC2_ECommerce.Services.LogServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _15PC2_ECommerce.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;            
        }

        // Listeleme
        public async Task<IActionResult> Index(string searchQuery, int page = 1, int pageSize = 10)
        {
            // 1️ Tüm kategorileri getir
            var categories = await _categoryService.GetAllCategoriesAsync();

            // 2️ Arama filtresi uygula (CategoryName'e göre)
            if (!string.IsNullOrEmpty(searchQuery))
            {
                categories = categories
                    .Where(c => c.CategoryName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // 3️ Sayfalama işlemi
            int totalItems = categories.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            categories = categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // 4️ ViewBag ile yardımcı bilgileri gönder
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;

            return View(categories);
        }

        // Yeni Kategori Ekle (GET)
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        // Yeni Kategori Ekle (POST)
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _categoryService.CreateCategoryAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Kategori Güncelle (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            var updateModel = _mapper.Map<UpdateCategoryDTO>(category);

            return View(updateModel);
        }

        // Kategori Güncelle (POST)
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _categoryService.UpdateCategoryAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Kategori Sil
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.RemoveCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

//public async Task<IActionResult> Index()
//{
//    var categories = await _categoryService.GetAllCategoriesAsync();
//    return View(categories);
//}
