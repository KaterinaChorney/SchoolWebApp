using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Repositories
{
    public class PositionRepository : GenericRepository<Position>, IPositionRepository
    {
        public PositionRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Position>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            IQueryable<Position> query = _context.Positions;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(lowerSearch));
            }

            query = sort?.ToLower() switch
            {
                "name" => query.OrderBy(p => p.Name),
                _ => query.OrderBy(p => p.Id)
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}