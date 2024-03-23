using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Profiles
{
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile()
        {
            // Mapeamento para Transferência
            CreateMap<CreateTransferenciaDto, Transacao>()
                .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(src => TipoTransacao.Transferencia))
                .ForMember(dest => dest.ContaOrigemId, opt => opt.MapFrom(src => src.ContaOrigemId))
                .ForMember(dest => dest.ContaDestinoId, opt => opt.MapFrom(src => src.ContaDestinoId))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));

            // Mapeamento para Saque
            // CreateMap<CreateSaqueDto, Transacao>()
            //     .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(src => TipoTransacao.Saque))
            //     .ForMember(dest => dest.NumeroContaOrigem, opt => opt.MapFrom(src => src.NumeroContaOrigem))
            //     .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));

            // // Mapeamento para Depósito
            // CreateMap<CreateDepositoDto, Transacao>()
            //     .ForMember(dest => dest.TipoTransacao, opt => opt.MapFrom(src => TipoTransacao.Deposito))
            //     .ForMember(dest => dest.NumeroContaDestino, opt => opt.MapFrom(src => src.NumeroContaDestino))
            //     .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));

            // // Mapeamento para Atualização de Transação
            // CreateMap<UpdateTransacaoDto, Transacao>()
            //     .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            //     .ForMember(dest => dest.DescricaoTransacao, opt => opt.MapFrom(src => src.DescricaoTransacao));
        }
    }
}
