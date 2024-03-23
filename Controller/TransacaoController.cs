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
        // Verifica se as contas de origem e destino existem no banco de dados
        var contaOrigem = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == transferenciaDto.ContaOrigemId);
        var contaDestino = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == transferenciaDto.ContaDestinoId);
        
        if (contaOrigem == null || contaDestino == null)
        {
            return StatusCode(400, "Conta de origem ou destino não encontrada.");
        }

        // Verifica se a conta de origem tem saldo suficiente
        if (contaOrigem.Saldo < transferenciaDto.Valor)
        {
            return StatusCode(400, "Saldo insuficiente na conta de origem.");
        }
        
        // Processa a transferência
        if (contaOrigem.NumeroConta != null && contaDestino.NumeroConta != null)
        {
            contaOrigem.Saldo -= transferenciaDto.Valor;
            contaDestino.Saldo += transferenciaDto.Valor;
        }
        else
        {
            throw new InvalidOperationException("Conta de origem ou destino não encontrada.");
        }

        // Cria a transação
        var transacao = _mapper.Map<Transacao>(transferenciaDto);
        transacao.TipoTransacao = TipoTransacao.Transferencia;
        transacao.ContaOrigemId = transferenciaDto.ContaOrigemId;
        transacao.ContaDestinoId = transferenciaDto.ContaDestinoId;
    
        

        // Adiciona a transação ao contexto e salva no banco de dados
        _appDbContext.Transacaos.Add(transacao);
        _appDbContext.SaveChanges();
        
        return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = transacao.Id }, transacao);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao processar transferência: {ex.Message}");
    }
}

[HttpPost("deposito")]
public IActionResult Depositar(CreateDepositoDto depositoDto)
{
    try
    {
        // Verifica se a conta de destino existe no banco de dados
        var contaDestino = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == depositoDto.ContaDestinoId);
        
        if (contaDestino == null)
        {
            return StatusCode(400, "Conta de destino não encontrada.");
        }

        // Verifica se o valor do depósito é válido
        if (depositoDto.Valor <= 0)
        {
            return StatusCode(400, "O valor do depósito deve ser maior que zero.");
        }

        // Processa o depósito
        contaDestino.Saldo += depositoDto.Valor;

        // Cria a transação
        var transacao = new Transacao
        {
            DataHoraTrasacao = DateTime.Now,
            TipoTransacao = TipoTransacao.Deposito,
            Valor = depositoDto.Valor,
            ContaDestinoId = depositoDto.ContaDestinoId
        };

        // Adiciona a transação ao contexto e salva no banco de dados
        _appDbContext.Transacaos.Add(transacao);
        _appDbContext.SaveChanges();
        
        return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = transacao.Id }, transacao);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao processar depósito: {ex.Message}");
    }
}

[HttpPost("saque")]
public IActionResult Sacar(CreateSaqueDto saqueDto)
{
    try
    {
        // Verifica se a conta de origem existe no banco de dados
        var contaOrigem = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == saqueDto.ContaOrigemId);
        
        if (contaOrigem == null)
        {
            return StatusCode(400, "Conta de origem não encontrada.");
        }

        // Verifica se o valor do saque é válido
        if (saqueDto.Valor <= 0)
        {
            return StatusCode(400, "O valor do saque deve ser maior que zero.");
        }

        // Verifica se a conta tem saldo suficiente para o saque
        if (contaOrigem.Saldo < saqueDto.Valor)
        {
            return StatusCode(400, "Saldo insuficiente na conta de origem.");
        }

        // Processa o saque
        contaOrigem.Saldo -= saqueDto.Valor;

        // Cria a transação de saque
        var transacao = new Transacao
        {
            DataHoraTrasacao = DateTime.Now,
            TipoTransacao = TipoTransacao.Saque,
            Valor = saqueDto.Valor,
            ContaOrigemId = saqueDto.ContaOrigemId
        };

        // Adiciona a transação ao contexto e salva no banco de dados
        _appDbContext.Transacaos.Add(transacao);
        _appDbContext.SaveChanges();
        
        return CreatedAtAction(nameof(ObterTransacaoPorId), new { id = transacao.Id }, transacao);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao processar saque: {ex.Message}");
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

        transacaoExistente.DescricaoTrasacao = transacaoDto.DescricaoTrasacao;

        _appDbContext.SaveChanges();

        return Ok(transacaoExistente);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Erro ao atualizar transação: {ex.Message}");
    }
}


// [HttpDelete("{id}")]
// public IActionResult ExcluirTransacao(int id)
// {
//     try
//     {
//         var transacao = _appDbContext.Transacaos.FirstOrDefault(t => t.Id == id);
//         if (transacao == null)
//             return NotFound("Transação não encontrada");

//         _appDbContext.Transacaos.Remove(transacao);
//         _appDbContext.SaveChanges();
//         return NoContent();
//     }
//     catch (Exception ex)
//     {
//         return StatusCode(500, $"Erro ao excluir transação: {ex.Message}");
//     }
// }

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