

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model
{
    public class Transacao : Entity
    {
        public DateTime DataHoraTrasacao { get; set; } 
        public string? DescricaoTrasacao { get; set; }

        public TipoTransacao TipoTransacao { get; set; }

        public double Valor { get; set; }
        public Transacao()
        {
            this.DataHoraTrasacao = DateTime.Now;
        }



        public int? ContaOrigemId { get; set; }
        public virtual ContaCorrente? ContaOrigem { get; set; }

        public int? ContaDestinoId { get; set; }
        public virtual ContaCorrente? ContaDestino { get; set; }

        // public void ProcessarTransacao()
        // {
        //     switch (TipoTransacao)
        //     {
        //         case TipoTransacao.Transferencia:
        //             ProcessarTransferencia();
        //             break;
        //         case TipoTransacao.Saque:
        //             ProcessarSaque();
        //             break;
        //         case TipoTransacao.Deposito:
        //             ProcessarDeposito();
        //             break;
        //         default:
        //             throw new InvalidOperationException("Tipo de transação não suportado.");
        //     }
        // }

        // private void ProcessarTransferencia()
        // {
        //     if (ContaOrigem?.NumeroConta != null && ContaDestino?.NumeroConta != null)
        //     {
        //         ContaOrigem.Saldo -= Valor;
        //         ContaDestino.Saldo += Valor;
        //     }
        //     else
        //     {
        //         throw new InvalidOperationException("Conta de origem ou destino não encontrada.");
        //     }
        // }

        private void ProcessarSaque()
        {
            if (ContaOrigem != null)
            {
                ContaOrigem.Saldo -= Valor;
            }
            else
            {
                throw new InvalidOperationException("Conta de origem não encontrada.");
            }
        }

        private void ProcessarDeposito()
        {
            if (ContaDestino != null)
            {
                ContaDestino.Saldo += Valor;
            }
            else
            {
                throw new InvalidOperationException("Conta de destino não encontrada.");
            }
        }


    }
}



    public enum TipoTransacao
    {
        Deposito,
        Saque,
        Transferencia,
        
    }




