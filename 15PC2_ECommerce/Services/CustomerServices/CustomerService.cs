using _15PC2_ECommerce.Context;
using _15PC2_ECommerce.DTOs.CategoryDTOs;
using _15PC2_ECommerce.DTOs.CustomerDTOs;
using _15PC2_ECommerce.Entities;
using _15PC2_ECommerce.Services.LogServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _15PC2_ECommerce.Services.CustomerServices
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public CustomerService(AppDbContext context, IMapper mapper, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task CreateCustomerAsync(CreateCustomerDTO createCustomerDTO)
        {
            var customer = _mapper.Map<Customer>(createCustomerDTO);
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Customer", "CREATE", $"Müşteri oluşturuldu! (Id: {customer.CustomerId})");
        }

        public async Task<List<ResultCustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<List<ResultCustomerDTO>>(customers);
        }

        public async Task<GetCustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            return _mapper.Map<GetCustomerDTO>(customer);
        }

        public async Task RemoveCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Customer", "DELETE", $"Müşteri silindi! (Id: {customer.CustomerId})");
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDTO updateCustomerDTO)
        {
            var customer = await _context.Customers.FindAsync(updateCustomerDTO.CustomerId);

            if (customer == null)
                return;

            _mapper.Map(updateCustomerDTO, customer);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Customer", "UPDATE", $"Müşteri güncellendi! (Id: {customer.CustomerId})");
        }
    }
}
