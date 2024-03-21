

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model
{
    public class Transacao : Entity
    {
        public DateTime DataHoraTrasacao { get; set; } 
        public string? DescricaoTrasacao { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public Transacao()
        {
            this.DataHoraTrasacao = DateTime.Now;
        }


        public int ContaId { get; set; }
        public Conta Conta { get; set; }




    }


    public enum TipoTransacao
    {
        Deposito,
        Saque,
        Transferencia,
        Pagamento,
    }
}