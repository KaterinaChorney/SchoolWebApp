using SchoolWebApplication.DTOs;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<TeacherDto> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateTeacherDto dto); 
        Task UpdateAsync(int id, UpdateTeacherDto dto);
        Task DeleteAsync(int id);
    }
}