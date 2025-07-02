using SchoolWebApplication.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Services.Interfaces
{
    public interface IJournalService
    {
        Task<IEnumerable<JournalDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<JournalDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateJournalDto dto);
        Task UpdateAsync(int id, UpdateJournalDto dto);
        Task DeleteAsync(int id);
    }
}