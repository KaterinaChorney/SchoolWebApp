using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IUnitOfWork
    {
        ITeacherRepository Teachers { get; }
        IGenericRepository<Student> Students { get; }
        IGenericRepository<Subject> Subjects { get; }
        IGenericRepository<Class> Classes { get; }
        IGenericRepository<Position> Positions { get; }
        IGenericRepository<Journal> Journals { get; }
        IGenericRepository<User> Users { get; }

        Task<int> SaveAsync();
    }
}
