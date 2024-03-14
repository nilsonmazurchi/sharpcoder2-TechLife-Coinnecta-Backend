namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model
{
    public class Endereco : Entity
    {
        public string? Logradouro { get; set; }
        public string? Cep { get; set; }
        public string? Uf { get; set; }
        public string? Cidade { get; set; }
        public string? Complemento { get; set; }
        public string? PontoRef {get; set;}
        public int? Numero {get; set;}
        public string? Bairro {get; set;}

        public int UsuarioId { get; set; }
        
        public Usuario? Usuario { get; set; }
    }
}