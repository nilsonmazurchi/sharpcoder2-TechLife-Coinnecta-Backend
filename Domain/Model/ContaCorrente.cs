namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

public class ContaCorrente : Conta{

  public double TaxaRendimento { get; set; }  = 0.005;
  public double Rendimento{ get; set; }
  public DateOnly TempoDeAberturaConta{ get; set; }
  public int? UsuarioId{ get; set; }
  public virtual Usuario? Usuario { get; set; }
//   DateTime dataAtual = DateTime.Now.DateOnly;

//   public TempoDeAberturaConta(DateTime dataAtual, )
//   {

//   }

//    TempoDeAberturaConta =  
//     {    
//       Tempo = DateTime.Now - dataCriacaoConta;
//       Tempo = Tempo.TotalDays / 30.4375;
//     }
    

//   public double CalcularRendimento(Balanco){
//     if(Balanco<=0){
//        throw new ArgumentException("Saldo inválido.");
//     }
//     Rendimento = Balanco * Math.Pow((1 +TaxaRendimento),Tempo);
//     Rendimento = Rendimento - Balanco;
//   }
//   public public void Transferir(ContaCorrente Origem, ContaCorrente Destino, double Valor)
//   {
//     // Atualizar saldo do usuário de origem
//     double BalancoOrigem = ContaOrigem.AtualizarBalanco(-Valor);

//     // Atualizar saldo do usuário de destino
//     double BalancoDestino = ContaDestino.AtualizarBalanco(Valor);

//     // Registrar transação
//     //Transacao transacao = new Transacao(
//       //  DateTime.Now,
//         //Valor,
//        // Origem,
//        // Destino
//     //);

    
// }

//   public double AtualizarBalanco(double Valor){
//       if (Valor <= 0)
//         {
//             throw new ArgumentException("Valor inválido.");
//         }
//       if(TipoTransacao.DEPOSITO){
//           Balanco += Valor;
//           return Balanco;
//       }
//       if(TipoTransacao.SAQUE){
//           Balanco -= Valor;
//           return Balanco;
//       }
//       if(TipoTransacao.TRANSFERENCIA){
        
//           Transferir();
        
         
//       }  
     
//   }
}

