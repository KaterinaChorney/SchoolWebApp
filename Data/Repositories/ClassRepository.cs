using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        public ClassRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Class>> GetAllAsync(string? search, string? sort, int page, int pageSize)
        {
            IQueryable<Class> query = _context.Classes;

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(c =>
                    c.Name.ToLower().Contains(lowerSearch) ||
                    c.ClassTeacher.ToLower().Contains(lowerSearch));
            }

            query = sort?.ToLower() switch
            {
                "name" => query.OrderBy(c => c.Name),
                "teacher" => query.OrderBy(c => c.ClassTeacher),
                _ => query.OrderBy(c => c.Id)
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}