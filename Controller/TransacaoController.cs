

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
{
    [ApiController]
    [Route("transacoes")]
    public class TransacaoController : ControllerBase
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TransacaoController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        


        [HttpPost("transferencia")]
public IActionResult Transferir(CreateTransferenciaDto transferenciaDto)
{
    try
    {
        var transacao = _mapper.Map<Transacao>(transferenciaDto);
        transacao.TipoTransacao = TipoTransacao.Transferencia;
        transacao.ProcessarTransacao();
        _appDbContext.Transacaos.Add(transacao);
        _appDbContext.SaveChanges();
        return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = transacao.Id }, transacao);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao processar transferência: {ex.Message}");
    }
}

[HttpPost("saque")]
public IActionResult Sacar(CreateSaqueDto saqueDto)
{
    try
    {
        var transacao = _mapper.Map<Transacao>(saqueDto);
        transacao.TipoTransacao = TipoTransacao.Saque;
        transacao.ProcessarTransacao();
        _appDbContext.Transacaos.Add(transacao);
        _appDbContext.SaveChanges();
        return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = transacao.Id }, transacao);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao processar saque: {ex.Message}");
    }
}

[HttpPost("deposito")]
public IActionResult Depositar(CreateDepositoDto depositoDto)
{
    try
    {
        var transacao = _mapper.Map<Transacao>(depositoDto);
        transacao.TipoTransacao = TipoTransacao.Deposito;
        transacao.ProcessarTransacao();
        _appDbContext.Transacaos.Add(transacao);
        _appDbContext.SaveChanges();
        return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = transacao.Id }, transacao);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao processar depósito: {ex.Message}");
    }
}

[HttpPut("{id}")]
public IActionResult AtualizarTransacao(int id, UpdateTransacaoDto transacaoDto)
{
    try
    {
        var transacaoExistente = _appDbContext.Transacaos.FirstOrDefault(t => t.Id == id);
        if (transacaoExistente == null)
            return NotFound("Transação não encontrada");

        transacaoExistente.Valor = transacaoDto.Valor;
        transacaoExistente.DescricaoTrasacao = transacaoDto.DescricaoTrasacao;

        _appDbContext.SaveChanges();

        return Ok(transacaoExistente);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao atualizar transação: {ex.Message}");
    }
}


[HttpDelete("{id}")]
public IActionResult ExcluirTransacao(int id)
{
    try
    {
        var transacao = _appDbContext.Transacaos.FirstOrDefault(t => t.Id == id);
        if (transacao == null)
            return NotFound("Transação não encontrada");

        _appDbContext.Transacaos.Remove(transacao);
        _appDbContext.SaveChanges();
        return NoContent();
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao excluir transação: {ex.Message}");
    }
}

[HttpGet("{id}")]
public IActionResult ObterTransacaoPorId(int id)
{
    try
    {
        var transacao = _appDbContext.Transacaos.FirstOrDefault(t => t.Id == id);
        if (transacao == null)
            return NotFound("Transação não encontrada");

        return Ok(transacao);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao obter transação por ID: {ex.Message}");
    }
}

 [HttpGet("Todos")]
        public IActionResult ObterTodasTransacoes()
        {
            try
            {
                var transacoes = _appDbContext.Transacaos.ToList();
                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter todas as transações: {ex.Message}");
            }
        }
    }
}