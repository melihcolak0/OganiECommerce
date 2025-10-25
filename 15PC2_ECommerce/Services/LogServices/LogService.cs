using _15PC2_ECommerce.Context;
using _15PC2_ECommerce.DTOs.CustomerDTOs;
using _15PC2_ECommerce.DTOs.LogDTOs;
using _15PC2_ECommerce.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace _15PC2_ECommerce.Services.LogServices
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public LogService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddLogAsync(string entityName, string operation, string description)
        {
            var log = new Log
            {
                EntityName = entityName,
                Operation = operation,
                Description = description,
                UserName = "Admin",
                LogDate = DateTime.UtcNow
            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();
        }       

        public async Task<List<ResultLogDTO>> GetAllLogsAsync()
        {
            var logs = await _context.Logs.ToListAsync();
            return _mapper.Map<List<ResultLogDTO>>(logs);
        }
    }
}

//public async Task AddLogAsync(CreateLogDTO createLogDTO)
//{
//    createLogDTO.UserName = "Admin";

//    var log = new Log
//    {
//        EntityName = createLogDTO.EntityName,
//        Operation = createLogDTO.Operation,
//        Description = createLogDTO.Description,
//        UserName = createLogDTO.UserName,
//        LogDate = DateTime.UtcNow
//    };

//    await _context.Logs.AddAsync(log);
//    await _context.SaveChangesAsync();
//}