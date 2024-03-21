

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
{
    [ApiController]
    [Route("Transacoes")]
    public class TransacaoController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TransacaoController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        


        [HttpPost]
        public IActionResult CriarTransacao(CreateTransacaoDto novaTransacaoDto)
        {
            try
            {
                var novaTransacao = _mapper.Map<Transacao>(novaTransacaoDto);
                _appDbContext.Transacaos.Add(novaTransacao);
                _appDbContext.SaveChanges();

                return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = novaTransacao.Id }, novaTransacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar transação: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterTransacaoPorId(int id)
        {
            try
            {
                var transacao = _appDbContext.Transacaos.Find(id);

                if (transacao == null)
                    return NotFound("Transação não encontrada");

                return Ok(transacao);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transação: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarTransacao(int id, CreateTransacaoDto transacaoAtualizadaDto)
        {
            try
            {
                var transacaoExistente = _appDbContext.Transacaos.Find(id);

                if (transacaoExistente == null)
                    return NotFound("Transação não encontrada");

                _mapper.Map(transacaoAtualizadaDto, transacaoExistente);
                _appDbContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar transação: {ex.Message}");
            }
        }

    }
}