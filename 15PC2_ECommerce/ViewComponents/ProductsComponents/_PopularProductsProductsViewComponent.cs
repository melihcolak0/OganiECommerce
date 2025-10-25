using _15PC2_ECommerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace _15PC2_ECommerce.ViewComponents.ProductsComponents
{
    public class _PopularProductsProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public _PopularProductsProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var popularProducts = await _productService.GetPopularProductsAsync();
            return View(popularProducts.Take(6).ToList());
        }
    }
}
