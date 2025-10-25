namespace _15PC2_ECommerce.DTOs.LogDTOs
{
    public class ResultLogDTO
    {
        public int LogId { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public string Operation { get; set; }
        public string? Description { get; set; }
        public DateTime LogDate { get; set; }
    }
}
