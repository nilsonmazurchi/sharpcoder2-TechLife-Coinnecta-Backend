namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

public class ContaPoupanca : Conta{   

    public double Rendimento { get; set; }
    
    public virtual Usuario? Usuario { get; set; }
    public int? UsuarioId{ get; set; }
}

