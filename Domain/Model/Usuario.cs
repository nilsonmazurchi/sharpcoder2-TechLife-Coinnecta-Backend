namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

    public class Usuario : Entity
    {
    private string? Nome {get; set;} 
    private string? Telefone {get;set;} 
    private string? Email {get;set;} 
    private string? Cpf {get; set;} 
    private string? Cnpj { get; set; }
    private DateOnly? DiaNascimento {get; set;} 
    private string? Senha {get; set;}
    private string? TipoPessoa {get; set;} 

    public Endereco? Endereco { get; set; }

    }
