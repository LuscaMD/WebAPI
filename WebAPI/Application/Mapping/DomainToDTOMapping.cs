using AutoMapper;
using WebAPI.Domain.DTOs;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Mapping
{
    public class DomainToDTOMapping : Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.Name, m => m.MapFrom(orig => orig.name));
        }
    }
}
