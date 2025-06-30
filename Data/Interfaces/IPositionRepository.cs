using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IPositionRepository : IGenericRepository<Position>
    {
        Task<IEnumerable<Position>> GetFilteredAsync(string? keyword, string? sortBy, int page, int pageSize);
    }
}