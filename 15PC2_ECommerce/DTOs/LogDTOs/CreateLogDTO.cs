namespace _15PC2_ECommerce.DTOs.LogDTOs
{
    public class CreateLogDTO
    {
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public string Operation { get; set; }
        public string? Description { get; set; }
    }
}
