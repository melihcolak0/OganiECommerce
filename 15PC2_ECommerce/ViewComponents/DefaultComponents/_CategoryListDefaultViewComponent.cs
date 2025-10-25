using Microsoft.AspNetCore.Mvc;

namespace _15PC2_ECommerce.ViewComponents.DefaultComponents
{
    public class _CategoryListDefaultViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
