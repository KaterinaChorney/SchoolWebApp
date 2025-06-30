using Microsoft.EntityFrameworkCore;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllWithClassAsync()
        {
            return await _context.Students
                .Include(s => s.Class)
                .ToListAsync();
        }
    }
}