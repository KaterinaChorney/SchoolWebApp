using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        Task<IEnumerable<Subject>> GetAllWithTeacherAsync();
        Task<Subject?> GetByIdWithTeacherAsync(int id);
    }
}