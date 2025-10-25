using _15PC2_ECommerce.Services.CategoryServices;
using _15PC2_ECommerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _15PC2_ECommerce.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public DefaultController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products(string category = null, decimal? minPrice = null, decimal? maxPrice = null, int page = 1, int pageSize = 12)
        {
            // Tüm ürünleri çek
            var products = await _productService.GetAllProductsOrderedAsync();

            // Fiyat aralığı filtreleme
            if (minPrice.HasValue && maxPrice.HasValue)
                products = products.Where(p => p.UnitPrice >= minPrice && p.UnitPrice <= maxPrice).ToList();

            // Kategori filtreleme
            if (!string.IsNullOrEmpty(category))
                products = products.Where(p => p.CategoryName == category).ToList();

            // Sayfalama işlemi
            int totalProducts = products.Count();
            var paginatedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var categories = await _categoryService.GetAllCategoriesAsync();

            ViewBag.Categories = categories
           .Select(c => c.CategoryName)
           .Distinct()
           .ToList();

            ViewBag.TotalProducts = totalProducts;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.Category = category;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View(paginatedProducts);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
