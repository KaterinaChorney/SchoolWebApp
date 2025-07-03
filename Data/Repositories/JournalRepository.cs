using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Repositories
{
    public class JournalRepository : GenericRepository<Journal>, IJournalRepository
    {
        public JournalRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Journal>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            IQueryable<Journal> query = _context.Journals
                .Include(j => j.Student)
                .Include(j => j.Subject)
                .Include(j => j.Class)
                .Include(j => j.Teacher);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(j =>
                    j.Student.FirstName.ToLower().Contains(lowerSearch) ||
                    j.Subject.Name.ToLower().Contains(lowerSearch));
            }

            bool descending = false;
            if (!string.IsNullOrWhiteSpace(sort))
            {
                sort = sort.Trim();
                if (sort.StartsWith("-"))
                {
                    descending = true;
                    sort = sort.Substring(1);
                }
            }

            query = sort?.ToLower() switch
            {
                "date" => descending ? query.OrderByDescending(j => j.Date) : query.OrderBy(j => j.Date),
                _ => query.OrderBy(j => j.Id)
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Journal?> GetByIdAsync(int id)
        {
            return await _context.Journals
                .Include(j => j.Student)
                .Include(j => j.Subject)
                .Include(j => j.Class)
                .Include(j => j.Teacher)
                .FirstOrDefaultAsync(j => j.Id == id);
        }
    }
}
