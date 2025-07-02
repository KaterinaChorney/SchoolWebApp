using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<Subject?> GetByIdWithTeacherAsync(int id);
    }
}
