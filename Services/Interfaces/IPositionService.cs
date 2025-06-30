using SchoolWebApplication.DTOs;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface IPositionService
    {
        Task<IEnumerable<PositionDto>> GetAllAsync();
        Task<PositionDto?> GetByIdAsync(int id);
        Task<IEnumerable<PositionDto>> GetFilteredAsync(string? keyword, string? sortBy, int page, int pageSize);
        Task CreateAsync(CreatePositionDto dto);
        Task UpdateAsync(int id, UpdatePositionDto dto);
        Task DeleteAsync(int id);
    }
}