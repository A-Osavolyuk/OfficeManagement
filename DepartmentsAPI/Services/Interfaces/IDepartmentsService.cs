using DepartmentsAPI.Models.DTOs;
using DepartmentsAPI.Models.Entities;
using LanguageExt.Common;

namespace DepartmentsAPI.Services.Interfaces
{
    public interface IDepartmentsService
    {
        ValueTask<Result<IEnumerable<Department>>> GetAll();
        ValueTask<Result<Department>> GetById(int id);
        ValueTask<Result<Department>> Create(DepartmentDto department);
        ValueTask<Result<Department>> Update(DepartmentDto department, int id);
        ValueTask<Result<bool>> DeleteById(int id);
    }
}
