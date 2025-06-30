using SchoolWebApplication.DTOs;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync();
        Task<SubjectDto?> GetByIdAsync(int id);
        Task<IEnumerable<SubjectDto>> GetFilteredAsync(string? keyword, string? sortBy, int page = 1, int pageSize = 10);
        Task CreateAsync(CreateSubjectDto dto);
        Task UpdateAsync(int id, UpdateSubjectDto dto);
        Task DeleteAsync(int id);
    }
}