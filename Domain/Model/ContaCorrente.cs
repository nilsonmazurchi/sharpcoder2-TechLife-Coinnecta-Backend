namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

public class ContaCorrente : Conta{
  public int TempoDeAberturaConta{ get; set; }
  public int? UsuarioId{ get; set; }
  public virtual Usuario? Usuario { get; set; }

}

