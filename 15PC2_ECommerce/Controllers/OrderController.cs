using _15PC2_ECommerce.DTOs.OrderDTOs;
using _15PC2_ECommerce.Services.CustomerServices;
using _15PC2_ECommerce.Services.OrderServices;
using _15PC2_ECommerce.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _15PC2_ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService, ICustomerService customerService, IProductService productService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
        }

        // Listeleme
        public async Task<IActionResult> Index(string searchQuery, int page = 1, int pageSize = 10)
        {
            var orders = await _orderService.GetAllOrdersAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                orders = orders
                    .Where(o => (o.CustomerName + " " + o.CustomerSurname + " " + o.ProductName)
                    .Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int totalItems = orders.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            orders = orders
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;

            return View(orders);
        }

        // Yeni Sipariş Ekle (GET)
        [HttpGet]
        public async Task<IActionResult> CreateOrder()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Customers = new SelectList(
                customers.Select(c => new {
                    CustomerId = c.CustomerId,
                    FullName = c.Name + " " + c.Surname
                }),
                "CustomerId",
                "FullName"
            );

            var products = await _productService.GetAllProductsAsync();
            ViewBag.Products = new SelectList(products, "ProductId", "ProductName");

            return View();
        }

        // Yeni Sipariş Ekle (POST)
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO model)
        {
            if (!ModelState.IsValid)
            {
                var customers = await _customerService.GetAllCustomersAsync();
                ViewBag.Customers = new SelectList(
                    customers.Select(c => new {
                        CustomerId = c.CustomerId,
                        FullName = c.Name + " " + c.Surname
                    }),
                    "CustomerId",
                    "FullName"
                );

                var products = await _productService.GetAllProductsAsync();
                ViewBag.Products = new SelectList(products, "ProductId", "ProductName");

                return View(model);
            }

            await _orderService.CreateOrderAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Sipariş Güncelle (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            var updateModel = new UpdateOrderDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                ProductId = order.ProductId,
                Amount = order.Amount,
                TotalPrice = order.TotalPrice,
                OrderDate = order.OrderDate
            };

            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Customers = new SelectList(
                customers.Select(c => new {
                    CustomerId = c.CustomerId,
                    FullName = c.Name + " " + c.Surname
                }),
                "CustomerId",
                "FullName"
            );

            var products = await _productService.GetAllProductsAsync();
            ViewBag.Products = new SelectList(products, "ProductId", "ProductName");

            return View(updateModel);
        }

        // Sipariş Güncelle (POST)
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDTO model)
        {
            if (!ModelState.IsValid)
            {
                var customers = await _customerService.GetAllCustomersAsync();
                ViewBag.Customers = new SelectList(
                    customers.Select(c => new {
                        CustomerId = c.CustomerId,
                        FullName = c.Name + " " + c.Surname
                    }),
                    "CustomerId",
                    "FullName"
                );

                var products = await _productService.GetAllProductsAsync();
                ViewBag.Products = new SelectList(products, "ProductId", "ProductName");

                return View(model);
            }

            await _orderService.UpdatOrderAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Sipariş Sil
        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.RemoveOrderAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
