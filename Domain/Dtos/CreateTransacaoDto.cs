


namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos
{
        public class CreateTransferenciaDto
    {
        public double Valor { get; set; }
        public int ContaOrigemId { get; set; }
        public int ContaDestinoId { get; set; }
    }


    
    public class CreateSaqueDto
    {
        public double Valor { get; set; }
        public int ContaOrigemId { get; set; }
    }

    
    public class CreateDepositoDto
    {
        public double Valor { get; set; }
        public int ContaDestinoId { get; set; }
    }



}