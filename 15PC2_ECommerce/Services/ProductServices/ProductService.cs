using _15PC2_ECommerce.Context;
using _15PC2_ECommerce.DTOs.ProductDTOs;
using _15PC2_ECommerce.Entities;
using _15PC2_ECommerce.Services.LogServices;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _15PC2_ECommerce.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public ProductService(AppDbContext context, IMapper mapper, ILogService logService)
        {
            _context = context;
            _mapper = mapper;
            _logService = logService;
        }

        public async Task CreateProductAsync(CreateProductDTO createProductDTO)
        {
            var product = _mapper.Map<Product>(createProductDTO);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Product", "CREATE", $"Ürün oluşturuldu! (Id: {product.ProductId})");
        }

        public async Task<List<ResultProductDTO>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            var result = products.Select(p => new ResultProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                Stock = p.Stock,
                CategoryName = p.Category.CategoryName
            }).ToList();

            return result;
        }

        public async Task<List<ResultProductDTO>> GetAllProductsOrderedAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderBy(x => x.ProductId)
                .ToListAsync();

            var result = products.Select(p => new ResultProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                UnitPrice = p.UnitPrice,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                Stock = p.Stock,
                CategoryName = p.Category.CategoryName
            }).ToList();

            return result;
        }

        public async Task<List<ResultProductDTO>> GetPopularProductsAsync()
        {
            var popularProductsQuery = _context.Orders
                                                .Include(o => o.Product)
                                                .ThenInclude(p => p.Category)
                                                .GroupBy(o => new { o.ProductId, o.Product.CategoryId })
                                                .Select(g => new
                                                {
                                                    ProductId = g.Key.ProductId,
                                                    CategoryId = g.Key.CategoryId,
                                                    TotalSold = g.Sum(x => x.Amount),
                                                    ProductName = g.First().Product.ProductName,
                                                    UnitPrice = g.First().Product.UnitPrice,
                                                    ImageUrl = g.First().Product.ImageUrl,
                                                    Stock = g.First().Product.Stock,
                                                    CategoryName = g.First().Product.Category.CategoryName
                                                })
                                                .OrderByDescending(x => x.TotalSold)
                                                .AsEnumerable() // Buradan itibaren LINQ to Objects
                                                .GroupBy(x => x.CategoryId) // Her kategoriden en popüler ürün
                                                .Select(g => g.First())
                                                .Take(15); // Toplam 15 ürün

            var result = popularProductsQuery
                .Select(p => new ResultProductDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    ImageUrl = p.ImageUrl,
                    Stock = p.Stock,
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName
                })
                .ToList();

            return await Task.FromResult(result);
        }

        public async Task<GetProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return null;

            var result = _mapper.Map<GetProductDTO>(product);
            result.CategoryName = product.Category?.CategoryName;
            return result;
        }

        public async Task RemoveProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Product", "DELETE", $"Ürün silindi! (Id: {product.ProductId})");
        }

        public async Task UpdatProductAsync(UpdateProductDTO updateProductDTO)
        {
            var product = await _context.Products.FindAsync(updateProductDTO.ProductId);

            if (product == null)
                return;

            _mapper.Map(updateProductDTO, product);
            await _context.SaveChangesAsync();

            await _logService.AddLogAsync("Product", "UPDATE", $"Ürün güncellendi! (Id: {product.ProductId})");
        }
    }
}
