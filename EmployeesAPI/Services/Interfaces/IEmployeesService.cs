using EmployeesAPI.Models.DTOs;
using EmployeesAPI.Models.Entities;
using LanguageExt.Common;

namespace EmployeesAPI.Services.Interfaces
{
    public interface IEmployeesService
    {
        public ValueTask<Result<IEnumerable<Employee>>> GetAll();
        public ValueTask<Result<IEnumerable<Employee>>> GetAllByPositionId(int positionId);
        public ValueTask<Result<Employee>> GetById(int id);
        public ValueTask<Result<Employee>> GetByEmail(string email);
        public ValueTask<Result<Employee>> Create(EmployeeDto employeeDto);
        public ValueTask<Result<Employee>> Update(int id, EmployeeDto employeeDto);
        public ValueTask<Result<bool>> DeleteById(int id);
    }
}
