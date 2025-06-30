using SchoolWebApplication.Entities;

namespace SchoolWebApplication.Data.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllWithClassAsync();
    }
}