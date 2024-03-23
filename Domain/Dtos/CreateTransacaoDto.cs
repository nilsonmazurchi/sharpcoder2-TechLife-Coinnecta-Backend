


namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos
{



    public class TransacaoDto
    {
        public double Valor { get; set; }
    }

    public class CreateTransferenciaDto : TransacaoDto
    {
        public int? ContaOrigemId { get; set; }
        
        public int? ContaDestinoId { get; set; }
        

        
    
    }

    public class CreateSaqueDto : TransacaoDto
    {
        public int ContaOrigemId { get; set; }
    }

    public class CreateDepositoDto : TransacaoDto
    {
        public int ContaDestinoId { get; set; }
    }

    public class UpdateTransacaoDto: TransacaoDto
    {
        
        public string? DescricaoTrasacao { get; set; }
    }

}