namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;


public class CreateContaPoupancaDto {
    public string? NumeroConta { get; set; }
    public DateTime DataAbertura = DateTime.Now;
    //public TipoConta TipoConta { get; set; }
    //public StatusConta StatusConta { get; set; }
    public double Saldo { get; set; } = 0;
    public double LimiteCredito { get; set; }
}


// public enum TipoConta
// {
//     ContaCorrente,
//     ContaPoupanca
// }

// public enum StatusConta
// {
//     Ativa,
//     Inativa
// }

