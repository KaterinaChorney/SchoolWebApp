using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Services.Interfaces;
using SchoolWebApplication.Data.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _unitOfWork.Students.GetAllWithClassAsync();
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = $"{s.LastName} {s.FirstName} {s.MiddleName}",
                ClassName = s.Class?.Name
            });
        }

        public async Task<StudentDto?> GetByIdAsync(int id)
        {
            var s = await _unitOfWork.Students.GetByIdAsync(id);
            if (s == null) return null;

            return new StudentDto
            {
                Id = s.Id,
                FullName = $"{s.LastName} {s.FirstName} {s.MiddleName}",
                ClassName = s.Class?.Name
            };
        }

        public async Task CreateAsync(CreateStudentDto dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                ClassId = dto.ClassId
            };

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateStudentDto dto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return;

            student.FirstName = dto.FirstName;
            student.LastName = dto.LastName;
            student.MiddleName = dto.MiddleName;
            student.ClassId = dto.ClassId;

            _unitOfWork.Students.Update(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return;

            _unitOfWork.Students.Delete(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<StudentDto>> GetFilteredAsync(string? keyword, string? sortBy, int page = 1, int pageSize = 10)
        {
            var students = await _unitOfWork.Students.GetAllWithClassAsync();
            var query = students.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(s =>
                    s.FirstName.ToLower().Contains(keyword) ||
                    s.LastName.ToLower().Contains(keyword) ||
                    s.MiddleName.ToLower().Contains(keyword));
            }

            query = sortBy?.ToLower() switch
            {
                "lastname" => query.OrderBy(s => s.LastName),
                "firstname" => query.OrderBy(s => s.FirstName),
                "class" => query.OrderBy(s => s.Class.Name),
                _ => query.OrderBy(s => s.Id)
            };

            return query
           .Skip((page - 1) * pageSize)
           .Take(pageSize)
           .ToList() 
           .Select(s => new StudentDto
           {
             Id = s.Id,
             FullName = $"{s.LastName} {s.FirstName} {s.MiddleName}",
             ClassName = s.Class != null ? s.Class.Name : null
           });
        }
    }
}