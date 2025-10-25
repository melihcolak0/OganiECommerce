using _15PC2_ECommerce.Services.CategoryServices;
using _15PC2_ECommerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _15PC2_ECommerce.ViewComponents.DefaultComponents
{
    public class _ProductListDefaultViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public _ProductListDefaultViewComponent(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _productService.GetPopularProductsAsync();
                                   
            ViewBag.Categories = await _categoryService.GetAllCategoriesAsync();

            return View(products);
        }
    }
}
