using AutoMapper;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Entities;
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

            CreateMap<Position, UpdatePositionViewModel>()
                .ForMember(x => x.PositionName, o => o.MapFrom(src => src.PositionName))
                .ForMember(x => x.DepartmentId, o => o.MapFrom(src => src.DepartmentId))
                .ForMember(x => x.PositionId, o => o.MapFrom(src => src.PositionId));

            CreateMap<UpdatePositionViewModel, PositionDto>()
                .ForMember(x => x.PositionName, o => o.MapFrom(src => src.PositionName))
                .ForMember(x => x.DepartmentId, o => o.MapFrom(src => src.DepartmentId));
        }
    }
}
