using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Repositories
{
    public class ClassRepository : GenericRepository<Class>, IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Class>> GetSortedAsync(string? keyword, string? sortBy, int page, int pageSize)
        {
            var query = _context.Classes.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(c => c.Name.ToLower().Contains(keyword.ToLower()) || c.ClassTeacher.ToLower().Contains(keyword.ToLower()));

            query = sortBy switch
            {
                "name" => query.OrderBy(c => c.Name),
                "teacher" => query.OrderBy(c => c.ClassTeacher),
                _ => query.OrderBy(c => c.Id)
            };

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
