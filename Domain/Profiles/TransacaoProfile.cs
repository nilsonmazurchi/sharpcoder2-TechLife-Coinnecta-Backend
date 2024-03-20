using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Profiles
{
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile() : base() {
      CreateMap<CreateTransacaoDto, Transacao>();
   }
    }
}
