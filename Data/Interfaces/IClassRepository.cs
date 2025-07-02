using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<IEnumerable<Class>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<Class?> GetByIdAsync(int id);
    }
}