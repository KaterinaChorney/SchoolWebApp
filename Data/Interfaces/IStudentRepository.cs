using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10);
        Task<Student?> GetByIdWithClassAsync(int id);
    }
}
