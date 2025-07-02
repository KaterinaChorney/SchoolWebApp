using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolWebApplication.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Student>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            IQueryable<Student> query = _context.Students.Include(s => s.Class);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerSearch = search.ToLower();
                query = query.Where(s =>
                    s.FirstName.ToLower().Contains(lowerSearch) ||
                    s.LastName.ToLower().Contains(lowerSearch));
            }

            query = sort?.ToLower() switch
            {
                "firstname" => query.OrderBy(s => s.FirstName),
                "lastname" => query.OrderBy(s => s.LastName),
                _ => query.OrderBy(s => s.Id)
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }

        public async Task<Student?> GetByIdWithClassAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}