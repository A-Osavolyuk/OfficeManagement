
using OfficeManagerBlazorServer.Models.DTOs;

namespace OfficeManagerBlazorServer.Services.Interfaces
{
    public interface IDepartmentHttpService
    {
        public ValueTask<ResponseDto> GetAll();
        public ValueTask<ResponseDto> GetById(int id);
        public ValueTask<ResponseDto> Update(int id, DepartmentDto department);
        public ValueTask<ResponseDto> DeleteById(int id);
        public ValueTask<ResponseDto> Create(DepartmentDto department);
    }
}
