using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Services.Interfaces;
using SchoolWebApplication.Data.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            var subjects = await _unitOfWork.Subjects.GetAllWithTeacherAsync();
            return subjects.Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                TeacherFullName = s.Teacher != null ? $"{s.Teacher.LastName} {s.Teacher.FirstName}" : null
            });
        }

        public async Task<SubjectDto?> GetByIdAsync(int id)
        {
            var s = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (s == null) return null;

            return new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                TeacherFullName = s.Teacher?.LastName + " " + s.Teacher?.FirstName
            };
        }

        public async Task<IEnumerable<SubjectDto>> GetFilteredAsync(string? keyword, string? sortBy, int page = 1, int pageSize = 10)
        {
            var subjects = await _unitOfWork.Subjects.GetAllWithTeacherAsync();
            var query = subjects.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(s => s.Name.ToLower().Contains(keyword.ToLower()));

            query = sortBy switch
            {
                "name" => query.OrderBy(s => s.Name),
                "teacher" => query.OrderBy(s => s.Teacher.LastName),
                _ => query.OrderBy(s => s.Id)
            };

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    TeacherFullName = s.Teacher != null ? $"{s.Teacher.LastName} {s.Teacher.FirstName}" : null
                });
        }

        public async Task CreateAsync(CreateSubjectDto dto)
        {
            var subject = new Subject
            {
                Name = dto.Name,
                TeacherId = dto.TeacherId
            };

            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateSubjectDto dto)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null) return;

            subject.Name = dto.Name;
            subject.TeacherId = dto.TeacherId;

            _unitOfWork.Subjects.Update(subject);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null) return;

            _unitOfWork.Subjects.Delete(subject);
            await _unitOfWork.SaveAsync();
        }
    }
}