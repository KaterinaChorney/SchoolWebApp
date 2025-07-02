using SchoolWebApplication.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<PositionDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreatePositionDto dto);
        Task UpdateAsync(int id, UpdatePositionDto dto);
        Task DeleteAsync(int id);
    }
}