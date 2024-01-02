using LanguageExt.Common;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Entities;

namespace OfficeManagerMVC.Services.Interfaces
{
    public interface IPositionsService
    {
        public ValueTask<ResponseDto> GetAll();
        public ValueTask<ResponseDto> GetAllByDepartmentId(int departmentId);
        public ValueTask<ResponseDto> GetById(int id);
        public ValueTask<ResponseDto> GetByName(string name);
        public ValueTask<ResponseDto> Create(PositionDto positionDto);
        public ValueTask<ResponseDto> Update(PositionDto positionDto, int id);
        public ValueTask<ResponseDto> DeleteById(int id);
    }
}
