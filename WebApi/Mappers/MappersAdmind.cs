using AutoMapper;
using Domain.DTOs.Responses;
using Domain.Entites;

namespace WebApi.Mappers
{
    public class MappersAdmind: Profile
    {
        public MappersAdmind() 
        {
            CreateMap<MovieResponseDTO, Movie>().ReverseMap();
            CreateMap<UserResponseDTO, User>().ReverseMap();
        }
    }
}
