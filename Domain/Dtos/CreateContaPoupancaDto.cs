namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;


public class CreateContaPoupancaDto {
    public string? NumeroConta { get; set; }
    public DateOnly DataAbertura { get; set;}
    public double Saldo { get; set; } = 0;
    public double Rendimento { get; set; }
}