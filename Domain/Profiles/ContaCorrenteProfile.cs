using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Profiles;

public class ContaCorrenteProfile : Profile
{
   public ContaCorrenteProfile() : base() {
      CreateMap<CreateContaCorrenteDto, ContaCorrente>();
   }
}
