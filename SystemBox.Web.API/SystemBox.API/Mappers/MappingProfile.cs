using AutoMapper;
using SystemBox.API.Dtos;
using SystemBox.Domain.Models;

namespace SystemBox.API.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Usuario, UsuarioDto>();
        }
    }
}
