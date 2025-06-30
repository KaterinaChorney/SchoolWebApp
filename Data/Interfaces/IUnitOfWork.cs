using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        ISubjectRepository Subjects { get; }
        IClassRepository Classes { get; }
        IGenericRepository<Position> Positions { get; }
        IGenericRepository<Journal> Journals { get; }
        IGenericRepository<User> Users { get; }

        Task<int> SaveAsync();
    }
}
