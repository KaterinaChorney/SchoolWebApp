using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Teachers = new TeacherRepository(context);
            Students = new StudentRepository(context);
            Subjects = new GenericRepository<Subject>(context);
            Classes = new GenericRepository<Class>(context);
            Positions = new GenericRepository<Position>(context);
            Journals = new GenericRepository<Journal>(context);
            Users = new GenericRepository<User>(context);
        }

        public ITeacherRepository Teachers { get; private set; }
        public IStudentRepository Students { get; private set; }
        public IGenericRepository<Subject> Subjects { get; private set; }
        public IGenericRepository<Class> Classes { get; private set; }
        public IGenericRepository<Position> Positions { get; private set; }
        public IGenericRepository<Journal> Journals { get; private set; }
        public IGenericRepository<User> Users { get; private set; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}