using AutoMapper;
using OnBoarding.api.Application.Commands;

namespace OnBoarding.api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Employee, Dto.EmployeeDto>().ReverseMap();

            CreateMap<CreateEmployeeCommand, Models.Employee>();
        }
    }
}
