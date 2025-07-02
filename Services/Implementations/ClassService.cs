using AutoMapper;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Exceptions;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClassDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            var classes = await _unitOfWork.Classes.GetAllAsync(search, sort, page, pageSize);
            return _mapper.Map<IEnumerable<ClassDto>>(classes);
        }

        public async Task<ClassDto> GetByIdAsync(int id)
        {
            var classEntity = await _unitOfWork.Classes.GetByIdAsync(id);
            if (classEntity == null)
                throw new NotFoundException($"Клас з ID = {id} не знайдено");

            return _mapper.Map<ClassDto>(classEntity);
        }

        public async Task<int> CreateAsync(CreateClassDto dto)
        {
            var classEntity = _mapper.Map<Class>(dto);
            await _unitOfWork.Classes.AddAsync(classEntity);
            await _unitOfWork.SaveAsync();
            return classEntity.Id;
        }

        public async Task UpdateAsync(int id, UpdateClassDto dto)
        {
            var classEntity = await _unitOfWork.Classes.GetByIdAsync(id);
            if (classEntity == null)
                throw new NotFoundException($"Клас з ID = {id} не знайдено");

            _mapper.Map(dto, classEntity);
            _unitOfWork.Classes.Update(classEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var classEntity = await _unitOfWork.Classes.GetByIdAsync(id);
            if (classEntity == null)
                throw new NotFoundException($"Клас з ID = {id} не знайдено");

            _unitOfWork.Classes.Delete(classEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}