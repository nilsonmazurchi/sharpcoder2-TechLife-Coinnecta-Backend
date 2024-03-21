using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos.Endereco;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace EnderecosController
{
    [ApiController]
    [Route("enderecos")]
    public class EnderecosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EnderecosController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> CadastroDeEndereco([FromBody] CreateEnderecoDto novoEnderecoDto)
        {
            var enderecoParaCadastro = _mapper.Map<Endereco>(novoEnderecoDto);

            var enderecoResult = await _appDbContext.Enderecos.AddAsync(enderecoParaCadastro);
            await _appDbContext.SaveChangesAsync();

            var enderecoSalvo = enderecoResult.Entity;

            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Id == enderecoSalvo.Id);

            if (usuario != null)
            {
                usuario.EnderecoId = enderecoSalvo.Id;
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return NotFound("Usuário não encontrado");
            }

            return CreatedAtAction(nameof(EnderecoPorId), new { Id = enderecoSalvo.Id }, enderecoSalvo);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> EnderecoPorId(int id)
        {
            var buscaEndereco = await _appDbContext.Enderecos.FindAsync(id);

            if (buscaEndereco == null)
                return NotFound();

            return Ok(buscaEndereco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var enderecoExistente = _appDbContext.Enderecos.FirstOrDefault(e => e.Id == id);
            if (enderecoExistente == null)
            {
                return NotFound("Endereço não encontrado");
            }
            
            enderecoExistente.Logradouro = enderecoDto.Logradouro;
            enderecoExistente.Cep = enderecoDto.Cep;
            enderecoExistente.Uf = enderecoDto.Uf;
            enderecoExistente.Cidade = enderecoDto.Cidade;
            enderecoExistente.Complemento = enderecoDto.Complemento;
            enderecoExistente.PontoRef = enderecoDto.PontoRef;
            enderecoExistente.Numero = enderecoDto.Numero;
            enderecoExistente.Bairro = enderecoDto.Bairro;
            
            await _appDbContext.SaveChangesAsync();

            return Ok("Endereço atualizado com sucesso");
        }

    }
}