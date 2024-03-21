namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

    public class Usuario : Entity
    {
    public string? Nome {get; set;} 
    public string? Ddd {get; set;}
    public string? Telefone {get;set;} 
    public string? Email {get;set;} 
    public string? Cpf {get; set;} 
    public string? Cnpj { get; set; }
    public DateOnly? DiaNascimento {get; set;} 
    public string? Senha {get; set;}
    public string? TipoPessoa {get; set;}
    public string? StatusUsuario { get; set; }

    public int? EnderecoId { get; set; }

    public virtual Endereco? Endereco {get; set;}

    public const string Ativo = "ativo";
    public const string Inativo = "inativo";

    }
