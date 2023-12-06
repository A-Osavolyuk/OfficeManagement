using AutoMapper;
using PositionsAPI.Models.DTOs;
using PositionsAPI.Models.Entities;

namespace PositionsAPI.Mapping
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<PositionDto, Position>();
            CreateMap<Position, PositionDto>();
        }
    }
}
