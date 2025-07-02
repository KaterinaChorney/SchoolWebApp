using SchoolWebApplication.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<SubjectDto> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateSubjectDto dto);
        Task UpdateAsync(int id, UpdateSubjectDto dto);
        Task DeleteAsync(int id);
    }
}