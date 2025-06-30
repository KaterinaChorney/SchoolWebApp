using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<IEnumerable<Class>> GetSortedAsync(string? keyword, string? sortBy, int page, int pageSize);
    }
}