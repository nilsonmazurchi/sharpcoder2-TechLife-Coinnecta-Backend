namespace sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos.Endereco
{
    public class UpdateEnderecoDto
    {
        public string? Logradouro { get; set; }
        public string? Cep { get; set; }
        public string? Uf { get; set; }
        public string? Cidade { get; set; }
        public string? Complemento { get; set; }
        public string? PontoRef { get; set; }
        public int? Numero { get; set; }
        public string? Bairro { get; set; }

    }
}