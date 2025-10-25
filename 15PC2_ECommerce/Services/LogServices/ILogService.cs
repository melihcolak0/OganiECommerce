using _15PC2_ECommerce.DTOs.LogDTOs;

namespace _15PC2_ECommerce.Services.LogServices
{
    public interface ILogService
    {
        Task<List<ResultLogDTO>> GetAllLogsAsync();
        Task AddLogAsync(string entityName, string operation, string description);      
    }
}

//Task AddLogAsync(CreateLogDTO createLogDTO);