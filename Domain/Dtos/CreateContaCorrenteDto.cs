namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
public class CreateContaCorrenteDto{

  	// public string? NumeroConta { get; set; }
    public DateTime DataAbertura {get ; set; }
    public TipoConta TipoConta { get; set; }
    public StatusConta StatusConta { get; set; }
    public double Saldo { get; set; }
  	public int TempoDeAberturaConta { get; set; }
  
  
}


public enum TipoConta
{
    Corrente,
    Poupanca
}

public enum StatusConta
{
    Ativa,
    Inativa
}


