using SchoolWebApplication.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface IClassService
    {
        Task<IEnumerable<ClassDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<ClassDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateClassDto dto);
        Task UpdateAsync(int id, UpdateClassDto dto);
        Task DeleteAsync(int id);
    }
}