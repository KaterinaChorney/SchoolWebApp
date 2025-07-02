using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IJournalRepository : IGenericRepository<Journal>
    {
        Task<IEnumerable<Journal>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<Journal?> GetByIdAsync(int id);
    }
}