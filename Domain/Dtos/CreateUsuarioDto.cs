using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos
{
    public class CreateUsuarioDto
    {
        public string? Nome {get; set;} 
        public string? Ddd {get; set; }
        public string? Telefone {get;set;} 
        public string? Email {get;set;} 
        public string? Cpf {get; set;} 
        public string? Cnpj { get; set; }
        public DateOnly? DiaNascimento {get; set;} 
        public string? Senha {get; set;}
        public string? TipoPessoa {get; set;} 
    }
}