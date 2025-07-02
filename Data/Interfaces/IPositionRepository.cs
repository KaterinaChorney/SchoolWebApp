using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        Task<IEnumerable<Position>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<Position?> GetByIdAsync(int id);
    }
}