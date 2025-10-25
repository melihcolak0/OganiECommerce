using _15PC2_ECommerce.Services.LogServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _15PC2_ECommerce.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<IActionResult> Index(string searchQuery, int page = 1, int pageSize = 5)
        {
            var logs = await _logService.GetAllLogsAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                logs = logs
                    .Where(l => (l.UserName + " " + l.EntityName + " " + l.Operation)
                    .Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int totalItems = logs.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            logs = logs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;

            return View(logs);
        }
    }
}
