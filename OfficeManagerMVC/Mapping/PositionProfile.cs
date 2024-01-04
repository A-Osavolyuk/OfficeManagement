using AutoMapper;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.ViewModels;

namespace OfficeManagerMVC.Mapping
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<CreatePositionViewModel, PositionDto>()
                .ForMember(x => x.PositionName, o => o.MapFrom(src => src.PositionName))
                .ForMember(x => x.DepartmentId, o => o.MapFrom(src => src.DepartmentId));
        }
    }
}
