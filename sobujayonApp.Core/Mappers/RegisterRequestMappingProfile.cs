using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;

namespace sobujayonApp.Core.Mappers;

public class RegisterRequestMappingProfile : Profile
{
  public RegisterRequestMappingProfile()
  {
    CreateMap<RegisterRequest, ApplicationUser>()
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
      .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
      ;
  }
}
