using _15PC2_ECommerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace _15PC2_ECommerce.ViewComponents.ProductsComponents
{
    public class _DiscountedProductsProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public _DiscountedProductsProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var discountedProducts = await _productService.GetPopularProductsAsync();          
            return View(discountedProducts);
        }
    }
}
