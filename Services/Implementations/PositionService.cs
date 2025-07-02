using AutoMapper;
using SchoolWebApplication.Data.Interfaces;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Exceptions;
using SchoolWebApplication.Services.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PositionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PositionDto>> GetAllAsync(string? search, string? sort, int page = 1, int pageSize = 10)
        {
            var positions = await _unitOfWork.Positions.GetAllAsync(search, sort, page, pageSize);
            return _mapper.Map<IEnumerable<PositionDto>>(positions);
        }

        public async Task<PositionDto> GetByIdAsync(int id)
        {
            var position = await _unitOfWork.Positions.GetByIdAsync(id);
            if (position == null)
                throw new NotFoundException($"Посаду з ID = {id} не знайдено");

            return _mapper.Map<PositionDto>(position);
        }

        public async Task<int> CreateAsync(CreatePositionDto dto)
        {
            var position = _mapper.Map<Position>(dto);
            await _unitOfWork.Positions.AddAsync(position);
            await _unitOfWork.SaveAsync();
            return position.Id;
        }

        public async Task UpdateAsync(int id, UpdatePositionDto dto)
        {
            var position = await _unitOfWork.Positions.GetByIdAsync(id);
            if (position == null)
                throw new NotFoundException($"Посаду з ID = {id} не знайдено");

            _mapper.Map(dto, position);
            _unitOfWork.Positions.Update(position);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var position = await _unitOfWork.Positions.GetByIdAsync(id);
            if (position == null)
                throw new NotFoundException($"Посаду з ID = {id} не знайдено");

            _unitOfWork.Positions.Delete(position);
            await _unitOfWork.SaveAsync();
        }
    }
}