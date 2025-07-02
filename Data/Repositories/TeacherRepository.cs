using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Repositories
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Teacher>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            IQueryable<Teacher> query = _context.Teachers.Include(t => t.Position);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(t =>
                    t.FirstName.ToLower().Contains(lowerSearch) ||
                    t.LastName.ToLower().Contains(lowerSearch) ||
                    t.MiddleName.ToLower().Contains(lowerSearch));
            }

            query = sort?.ToLower() switch
            {
                "firstname" => query.OrderBy(t => t.FirstName),
                "lastname" => query.OrderBy(t => t.LastName),
                "experience" => query.OrderByDescending(t => t.Experience),
                _ => query.OrderBy(t => t.Id)
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Teacher?> GetByIdWithPositionAsync(int id)
        {
            return await _context.Teachers
                .Include(t => t.Position)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Teacher>> GetTeachersWithSubjectsAsync()
        {
            return await _context.Teachers
                .Include(t => t.Position)
                .Include(t => t.Subjects)
                .ToListAsync();
        }
    }
}