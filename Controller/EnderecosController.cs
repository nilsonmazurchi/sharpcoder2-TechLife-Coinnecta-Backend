using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
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
        public IActionResult CadastroDeEndereco(CreateEnderecoDto novoEnderecoDto)
        {

            var enderecoParaCadastro = _mapper.Map<Endereco>(novoEnderecoDto);

            var result = _appDbContext.Enderecos.Add(enderecoParaCadastro);
            _appDbContext.SaveChanges();

            var enderecoSalvo = result.Entity;

            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Id == enderecoSalvo.Id);

            if (usuario != null)
            {
                usuario.EnderecoId = enderecoSalvo.Id;
                _appDbContext.SaveChanges();
            }
            else
            {
                return NotFound("Usuário não encontrado");
            }

            return CreatedAtAction(nameof(EnderecoPorId), new { Id = enderecoSalvo.Id }, enderecoSalvo);
        }


        [HttpGet("{id:int}")]
        public IActionResult EnderecoPorId(int id)
        {
            var buscaEndereco = _appDbContext.Enderecos.Find(id);

            if (buscaEndereco == null)
                return NotFound();

            return Ok(buscaEndereco);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarEndereco(int id, [FromBody] CreateEnderecoDto enderecoDto)
        {
            //verifica se o endereço com o id fornecido existe no db
            var enderecoExistente = _appDbContext.Enderecos.FirstOrDefault(e => e.Id == id);
            if (enderecoExistente == null)
            {
                return NotFound("Endereço não encontrado");
            }

            _mapper.Map(enderecoDto, enderecoExistente);
            _appDbContext.SaveChanges();

            return Ok("Endereço atualizado com sucesso");
        }
    }
}