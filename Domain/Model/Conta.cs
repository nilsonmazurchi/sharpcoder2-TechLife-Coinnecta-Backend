namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

    public class Conta : Entity
    {
    public string? NumeroConta { get; set; }
    public DateOnly DataAbertura = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    public string? TipoConta { get; set; }
    public string? StatusConta { get; set; }
    public double Saldo { get; set; } 

    public const string Ativo = "ativo";
    public const string Inativo = "inativo";

    public const string Corrente = "corrente";
    public const string Poupança = "poupança";

    }
