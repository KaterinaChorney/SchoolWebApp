using AutoMapper;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Exceptions;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class JournalService : IJournalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JournalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<JournalDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            var journals = await _unitOfWork.Journals.GetAllAsync(search, sort, page, pageSize);
            return _mapper.Map<IEnumerable<JournalDto>>(journals);
        }

        public async Task<JournalDto> GetByIdAsync(int id)
        {
            var journal = await _unitOfWork.Journals.GetByIdAsync(id);
            if (journal == null)
                throw new NotFoundException($"Журнал з ID = {id} не знайдено");

            return _mapper.Map<JournalDto>(journal);
        }

        public async Task<int> CreateAsync(CreateJournalDto dto)
        {
            var journal = _mapper.Map<Journal>(dto);
            await _unitOfWork.Journals.AddAsync(journal);
            await _unitOfWork.SaveAsync();
            return journal.Id;
        }

        public async Task UpdateAsync(int id, UpdateJournalDto dto)
        {
            var journal = await _unitOfWork.Journals.GetByIdAsync(id);
            if (journal == null)
                throw new NotFoundException($"Журнал з ID = {id} не знайдено");

            _mapper.Map(dto, journal);
            _unitOfWork.Journals.Update(journal);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var journal = await _unitOfWork.Journals.GetByIdAsync(id);
            if (journal == null)
                throw new NotFoundException($"Журнал з ID = {id} не знайдено");

            _unitOfWork.Journals.Delete(journal);
            await _unitOfWork.SaveAsync();
        }
    }
}