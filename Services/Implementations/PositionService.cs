using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Services.Interfaces;
using SchoolWebApplication.Data.Interfaces;

namespace SchoolWebApplication.Services.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PositionDto>> GetAllAsync()
        {
            var positions = await _unitOfWork.Positions.GetAllAsync();
            return positions.Select(p => new PositionDto { Id = p.Id, Name = p.Name });
        }

        public async Task<PositionDto?> GetByIdAsync(int id)
        {
            var p = await _unitOfWork.Positions.GetByIdAsync(id);
            return p == null ? null : new PositionDto { Id = p.Id, Name = p.Name };
        }

        public async Task<IEnumerable<PositionDto>> GetFilteredAsync(string? keyword, string? sortBy, int page, int pageSize)
        {
            var positions = await _unitOfWork.Positions.GetFilteredAsync(keyword, sortBy, page, pageSize);
            return positions.Select(p => new PositionDto { Id = p.Id, Name = p.Name });
        }

        public async Task CreateAsync(CreatePositionDto dto)
        {
            await _unitOfWork.Positions.AddAsync(new Position { Name = dto.Name });
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(int id, UpdatePositionDto dto)
        {
            var position = await _unitOfWork.Positions.GetByIdAsync(id);
            if (position == null) return;

            position.Name = dto.Name;
            _unitOfWork.Positions.Update(position);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var position = await _unitOfWork.Positions.GetByIdAsync(id);
            if (position == null) return;

            _unitOfWork.Positions.Delete(position);
            await _unitOfWork.SaveAsync();
        }
    }
}