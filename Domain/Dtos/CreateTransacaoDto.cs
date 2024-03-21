
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos
{
    public class CreateTransacaoDto
    {
        
        public DateTime DataHoraTrasacao { get; set; } 
        public string? DescricaoTrasacao { get; set; }

        public TipoTransacao TipoTransacao { get; set; }
    }
}