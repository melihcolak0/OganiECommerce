using _15PC2_ECommerce.DTOs.CustomerDTOs;
using _15PC2_ECommerce.Services.CustomerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _15PC2_ECommerce.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        // Listeleme
        public async Task<IActionResult> Index(string searchQuery, int page = 1, int pageSize = 10)
        {
            var customers = await _customerService.GetAllCustomersAsync();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                customers = customers
                    .Where(c => (c.Name + " " + c.Surname + " " + c.Email + " " + c.Phone + " " + c.City)
                    .Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            int totalItems = customers.Count;
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            customers = customers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;

            return View(customers);
        }       

        // Yeni Müşteri Ekle (GET)
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        // Yeni Müşteri Ekle (POST)
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _customerService.CreateCustomerAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Müşteri Güncelle (GET)
        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            var updateModel = _mapper.Map<UpdateCustomerDTO>(customer);

            return View(updateModel);
        }

        // Müşteri Güncelle (POST)
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _customerService.UpdateCustomerAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // Müşteri Sil
        [HttpGet]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.RemoveCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
