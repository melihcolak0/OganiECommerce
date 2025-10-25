using _15PC2_ECommerce.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace _15PC2_ECommerce.ViewComponents.DefaultComponents
{
    public class _CategorySidebarDefaultViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _CategorySidebarDefaultViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
    }
}
