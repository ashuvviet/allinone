using AutoMapper;
using moviebooking_api.DTOs;
using moviebooking_api.Model;
using System.Data.SqlTypes;

namespace moviebooking_api.AutoMapper
{
    internal sealed class MappingProfile : Profile
    {
        /// <summary>
        /// Creating a Mapping Profile for Auto mapper
        /// </summary>
        public MappingProfile()
        {
            CreateMap<CinemaDto, City>()
               .ForMember(d => d.Name, s => s.MapFrom(s1 => s1.City));

            CreateMap<CinemaDto, Cinema>();

            CreateMap<MovieDto, Movie>();

            CreateMap<MovieDto, CinemaMovie>();
        }
    }
}
