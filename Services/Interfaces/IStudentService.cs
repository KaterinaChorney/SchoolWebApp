using SchoolWebApplication.DTOs;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<IEnumerable<StudentDto>> GetFilteredAsync(string? keyword, string? sortBy, int page = 1, int pageSize = 10);

        Task<StudentDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateStudentDto dto);
        Task UpdateAsync(int id, UpdateStudentDto dto);
        Task DeleteAsync(int id);
    }
}