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
            Subjects = new SubjectRepository(context);
            Classes = new ClassRepository(context);
            Positions = new PositionRepository(context);
            Journals = new GenericRepository<Journal>(context);
            Users = new GenericRepository<User>(context);
        }

        public ITeacherRepository Teachers { get; private set; }
        public IStudentRepository Students { get; private set; }
        public ISubjectRepository Subjects { get; private set; }
        public IClassRepository Classes { get; private set; }
        public IPositionRepository Positions { get; private set; }
        public IGenericRepository<Journal> Journals { get; private set; }
        public IGenericRepository<User> Users { get; private set; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}