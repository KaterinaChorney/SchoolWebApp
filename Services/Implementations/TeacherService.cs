using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeacherService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            var query = await _unitOfWork.Teachers.GetTeachersWithSubjectsAsync();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t =>
                    t.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    t.LastName.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            query = sort switch
            {
                "experience_desc" => query.OrderByDescending(t => t.Experience),
                "experience_asc" => query.OrderBy(t => t.Experience),
                "name_asc" => query.OrderBy(t => t.LastName),
                "name_desc" => query.OrderByDescending(t => t.LastName),
                _ => query.OrderBy(t => t.Id)
            };

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return query.Select(t => new TeacherDto
            {
                Id = t.Id,
                FullName = $"{t.LastName} {t.FirstName} {t.MiddleName}",
                Experience = t.Experience,
                PositionName = t.Position?.Name
            });
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            var t = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (t == null) return null;

            return new TeacherDto
            {
                Id = t.Id,
                FullName = $"{t.LastName} {t.FirstName} {t.MiddleName}",
                Experience = t.Experience,
                PositionName = t.Position?.Name
            };
        }

        public async Task CreateAsync(CreateTeacherDto dto)
        {
            var teacher = new Teacher
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName,
                Experience = dto.Experience,
                PositionId = dto.PositionId
            };

            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateTeacherDto dto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null) return;

            teacher.FirstName = dto.FirstName;
            teacher.LastName = dto.LastName;
            teacher.MiddleName = dto.MiddleName;
            teacher.Experience = dto.Experience;
            teacher.PositionId = dto.PositionId;

            _unitOfWork.Teachers.Update(teacher);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null) return;

            _unitOfWork.Teachers.Delete(teacher);
            await _unitOfWork.SaveAsync();
        }
    }
}