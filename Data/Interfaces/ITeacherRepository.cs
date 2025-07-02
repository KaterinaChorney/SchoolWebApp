using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<Teacher?> GetByIdWithPositionAsync(int id);
    }
}