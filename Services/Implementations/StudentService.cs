using AutoMapper;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Exceptions;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            var students = await _unitOfWork.Students.GetAllAsync(search, sort, page, pageSize);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdWithClassAsync(id);
            if (student == null)
                throw new NotFoundException($"Студента з ID = {id} не знайдено");

            return _mapper.Map<StudentDto>(student);
        }

        public async Task<int> CreateAsync(CreateStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);
            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveAsync();
            return student.Id;
        }

        public async Task UpdateAsync(int id, UpdateStudentDto dto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException($"Студента з ID = {id} не знайдено");

            _mapper.Map(dto, student);
            _unitOfWork.Students.Update(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null)
                throw new NotFoundException($"Студента з ID = {id} не знайдено");

            _unitOfWork.Students.Delete(student);
            await _unitOfWork.SaveAsync();
        }
    }
}