using _15PC2_ECommerce.DTOs.ProductDTOs;
using _15PC2_ECommerce.Services.CategoryServices;
using _15PC2_ECommerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _15PC2_ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // Listeleme
        public async Task<IActionResult> Index(string searchQuery, int page = 1, int pageSize = 10)
        {
            var products = await _productService.GetAllProductsAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products
                    .Where(p => (p.ProductName + " " + p.CategoryName)
                    .Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int totalItems = products.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            products = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;

            return View(products);
        }

        // Yeni Ürün Ekle (GET)
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            return View();
        }

        // Yeni Ürün Ekle (POST)
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _productService.CreateProductAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Ürün Güncelle (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            var updateModel = new UpdateProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                ImageUrl = product.ImageUrl,
                Stock = product.Stock,
                CategoryId = product.CategoryId
            };

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            return View(updateModel);
        }

        // Ürün Güncelle (POST)
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _productService.UpdatProductAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Ürün Sil
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.RemoveProductAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
