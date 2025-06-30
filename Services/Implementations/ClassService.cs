using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Services.Interfaces;
using SchoolWebApplication.Data.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClassDto>> GetAllAsync()
        {
            var list = await _unitOfWork.Classes.GetAllAsync();
            return list.Select(c => new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                ClassTeacher = c.ClassTeacher
            });
        }

        public async Task<IEnumerable<ClassDto>> GetFilteredAsync(string? keyword, string? sortBy, int page, int pageSize)
        {
            var list = await _unitOfWork.Classes.GetSortedAsync(keyword, sortBy, page, pageSize);
            return list.Select(c => new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                ClassTeacher = c.ClassTeacher
            });
        }

        public async Task<ClassDto?> GetByIdAsync(int id)
        {
            var c = await _unitOfWork.Classes.GetByIdAsync(id);
            return c == null ? null : new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                ClassTeacher = c.ClassTeacher
            };
        }

        public async Task CreateAsync(CreateClassDto dto)
        {
            var c = new Class
            {
                Name = dto.Name,
                ClassTeacher = dto.ClassTeacher
            };
            await _unitOfWork.Classes.AddAsync(c);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdateClassDto dto)
        {
            var c = await _unitOfWork.Classes.GetByIdAsync(id);
            if (c == null) return;

            c.Name = dto.Name;
            c.ClassTeacher = dto.ClassTeacher;

            _unitOfWork.Classes.Update(c);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _unitOfWork.Classes.GetByIdAsync(id);
            if (c == null) return;

            _unitOfWork.Classes.Delete(c);
            await _unitOfWork.SaveAsync();
        }
    }
}