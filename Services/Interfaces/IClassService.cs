using SchoolWebApplication.DTOs;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDto>> GetAllAsync();
        Task<IEnumerable<ClassDto>> GetFilteredAsync(string? keyword, string? sortBy, int page, int pageSize);
        Task<ClassDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateClassDto dto);
        Task UpdateAsync(int id, UpdateClassDto dto);
        Task DeleteAsync(int id);
    }
}