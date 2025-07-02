using AutoMapper;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Exceptions;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            var subjects = await _unitOfWork.Subjects.GetAllAsync(search, sort, page, pageSize);
            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }

        public async Task<SubjectDto> GetByIdAsync(int id)
        {
            var subject = await _unitOfWork.Subjects.GetByIdWithTeacherAsync(id);
            if (subject == null)
                throw new NotFoundException($"Предмет з ID = {id} не знайдено");

            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task<int> CreateAsync(CreateSubjectDto dto)
        {
            var subject = _mapper.Map<Subject>(dto);
            await _unitOfWork.Subjects.AddAsync(subject);
            await _unitOfWork.SaveAsync();
            return subject.Id;
        }

        public async Task UpdateAsync(int id, UpdateSubjectDto dto)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null)
                throw new NotFoundException($"Предмет з ID = {id} не знайдено");

            _mapper.Map(dto, subject);
            _unitOfWork.Subjects.Update(subject);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (subject == null)
                throw new NotFoundException($"Предмет з ID = {id} не знайдено");

            _unitOfWork.Subjects.Delete(subject);
            await _unitOfWork.SaveAsync();
        }
    }
}