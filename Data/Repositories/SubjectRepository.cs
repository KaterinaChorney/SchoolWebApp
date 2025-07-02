using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Subject>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            IQueryable<Subject> query = _context.Subjects.Include(s => s.Teacher);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(s =>
                    s.Name.ToLower().Contains(lowerSearch));
            }

            query = sort?.ToLower() switch
            {
                "name" => query.OrderBy(s => s.Name),
                _ => query.OrderBy(s => s.Id)
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Subject?> GetByIdWithTeacherAsync(int id)
        {
            return await _context.Subjects
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}