using AutoMapper;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Exceptions; 
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeacherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            var teachers = await _unitOfWork.Teachers.GetAllAsync(search, sort, page, pageSize);
            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdWithPositionAsync(id);
            if (teacher == null)
                throw new NotFoundException($"Викладача з ID = {id} не знайдено");

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<int> CreateAsync(CreateTeacherDto dto)
        {
            var teacher = _mapper.Map<Teacher>(dto);
            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.SaveAsync();
            return teacher.Id;
        }

        public async Task UpdateAsync(int id, UpdateTeacherDto dto)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
                throw new NotFoundException($"Викладача з ID = {id} не знайдено");

            _mapper.Map(dto, teacher);
            _unitOfWork.Teachers.Update(teacher);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            if (teacher == null)
                throw new NotFoundException($"Викладача з ID = {id} не знайдено");

            _unitOfWork.Teachers.Delete(teacher);
            await _unitOfWork.SaveAsync();
        }
    }
}