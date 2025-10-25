using _15PC2_ECommerce.Services.ForecastServices;
using Microsoft.AspNetCore.Mvc;

namespace _15PC2_ECommerce.Controllers
{
    public class ForecastController : Controller
    {
        private readonly ForecastService _forecastService;

        public ForecastController(ForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        public async Task<IActionResult> Index()
        {
            var forecasts = await _forecastService.GetForecastWithHistoryAsync();
            return View(forecasts);
        }
    }
}
