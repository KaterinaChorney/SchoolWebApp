using SchoolWebApplication.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<StudentDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateStudentDto dto);
        Task UpdateAsync(int id, UpdateStudentDto dto);
        Task DeleteAsync(int id);
    }
}