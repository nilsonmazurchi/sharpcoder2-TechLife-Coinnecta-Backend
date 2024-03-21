using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos.Usuario;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Profiles;

public class UsuarioProfile : Profile
{
   public UsuarioProfile() : base() {
      CreateMap<CreateUsuarioDto, Usuario>();
      CreateMap<UpdateUsuarioDto, Usuario>();

   }
}
