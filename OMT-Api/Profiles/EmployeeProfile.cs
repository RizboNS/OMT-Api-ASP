using AutoMapper;
using OMT_Api.Entities;
using OMT_Api.Models;

namespace OMT_Api.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeRegisterDto, Employee>();
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(
                dest => dest.Role,
                opt => opt.MapFrom(src => Enum.GetName(typeof(Role), src.Role))
                );
        }
    }
}