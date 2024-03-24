

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model
{
    public class Transacao : Entity
    {
        public DateTime DataHoraTrasacao { get; set; } = DateTime.Now;
        public string? DescricaoTrasacao { get; set; }

        public string? TipoTransacao { get; set; }

        public double Valor { get; set; }
        
        public const string Deposito = "deposito";
        public const string Saque = "saque";
        public const string Transferencia = "transferencia";

        public int? ContaOrigemId { get; set; }
        public virtual ContaCorrente? ContaOrigem { get; set; }

        public int? ContaDestinoId { get; set; }
        public virtual ContaCorrente? ContaDestino { get; set; }
    }
}

