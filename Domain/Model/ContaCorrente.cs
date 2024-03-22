namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

public class ContaCorrente : Conta{
  public double LimiteCredito { get; set; } = 100.00;
  public int? UsuarioId{ get; set; }
  public virtual Usuario? Usuario { get; set; }

  

}
