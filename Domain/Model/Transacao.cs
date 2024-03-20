using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model
{
    public class Transacao : Entity
    {
        public DateTime DataRealizacao {get; set; }
        public string? Descricao { get; set; }
        public TipoTransacao TipoTransacao{ get; set; }
                
        public Double ValorTransacao {get; set;}
        public Double Saldo {get; set; }
        public int ContaCorrenteId { get; set; }

}

public enum TipoTransacao
{
    Deposito,
    
    Saque,
    Transferencia
}
}