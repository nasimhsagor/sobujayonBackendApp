using AutoMapper;
using sobujayonApp.Core.DTO;
using sobujayonApp.Core.Entities;

namespace sobujayonApp.Core.Mappers;

public class ApplicationUserMappingProfile : Profile
{
  public ApplicationUserMappingProfile()
  {
    CreateMap<ApplicationUser, AuthenticationResponse>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
      .ForMember(dest => dest.Success, opt => opt.Ignore())
      .ForMember(dest => dest.Token, opt => opt.Ignore())
      ;
  }
}
