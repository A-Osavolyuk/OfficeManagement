using LanguageExt.Common;
using PositionsAPI.Models.DTOs;
using PositionsAPI.Models.Entities;

namespace PositionsAPI.Services.Interfaces
{
    public interface IPositionsService
    {
        public ValueTask<Result<IEnumerable<Position>>> GetAll();
        public ValueTask<Result<IEnumerable<Position>>> GetAllByDepartmentId(int departmentId);
        public ValueTask<Result<Position>> GetById(int id);
        public ValueTask<Result<Position>> GetByName(string name);
        public ValueTask<Result<Position>> Create(PositionDto positionDto);
        public ValueTask<Result<Position>> Update(PositionDto positionDto, int id);
        public ValueTask<Result<bool>> DeleteById(int id);
    }
}
