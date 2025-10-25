using _15PC2_ECommerce.Entities;

namespace _15PC2_ECommerce.DTOs.OrderDTOs
{
    public class CreateOrderDTO
    {
        public int CustomerId { get; set; }        
        public decimal Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
    }
}
