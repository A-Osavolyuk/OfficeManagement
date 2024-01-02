using OfficeManagerMVC.Models.DTOs;

namespace OfficeManagerMVC.Services.Interfaces
{
    public interface IEmployeesService
    {
        public ValueTask<ResponseDto> GetAll();
        public ValueTask<ResponseDto> GetAllByPositionId(int positionId);
        public ValueTask<ResponseDto> GetById(int id);
        public ValueTask<ResponseDto> GetByEmail(string email);
        public ValueTask<ResponseDto> Create(EmployeeDto employeeDto);
        public ValueTask<ResponseDto> Update(int id, EmployeeDto employeeDto);
        public ValueTask<ResponseDto> DeleteById(int id);
    }
}
