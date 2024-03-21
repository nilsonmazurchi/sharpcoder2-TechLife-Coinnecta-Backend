using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Profiles
{
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile()
        {
            CreateMap<CreateTransferenciaDto, Transacao>()
                .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(src => TipoTransacao.Transferencia));

            CreateMap<CreateSaqueDto, Transacao>()
                .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(src => TipoTransacao.Saque));

            CreateMap<CreateDepositoDto, Transacao>()
                .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(src => TipoTransacao.Deposito));

            CreateMap<UpdateTransacaoDto, Transacao>()
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
                .ForMember(dest => dest.DescricaoTrasacao, opt => opt.MapFrom(src => src.DescricaoTrasacao));
        }
    }
}
