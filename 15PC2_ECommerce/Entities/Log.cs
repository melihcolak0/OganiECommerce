namespace _15PC2_ECommerce.Entities
{
    public class Log
    {
        public int LogId { get; set; }
        public string UserName { get; set; }
        public string EntityName { get; set; }
        public string Operation { get; set; }
        public string? Description { get; set; }
        public DateTime LogDate { get; set; }
    }
}
