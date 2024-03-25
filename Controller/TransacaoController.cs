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
        public IActionResult Transferir(CreateTransferenciaDto operacao)
        {
            try
            {
                var contaOrigem = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == operacao.ContaOrigemId);
                var contaDestino = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == operacao.ContaDestinoId);

                if (contaOrigem == null || contaDestino == null)
                {
                    return StatusCode(400, "Conta de origem ou destino não encontrada.");
                }

                if (contaOrigem.Saldo < operacao.Valor)
                {
                    return StatusCode(400, "Saldo insuficiente na conta de origem.");
                }

                if (contaOrigem.NumeroConta != null && contaDestino.NumeroConta != null)
                {
                    contaOrigem.Saldo -= operacao.Valor;
                    contaDestino.Saldo += operacao.Valor;
                }
                else
                {
                    throw new InvalidOperationException("Conta de origem ou destino não encontrada.");
                }

                var transacao = _mapper.Map<Transacao>(operacao);
                transacao.TipoTransacao = "transferencia";
                transacao.ContaOrigemId = operacao.ContaOrigemId;
                transacao.ContaDestinoId = operacao.ContaDestinoId;


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
                var contaDestino = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == depositoDto.ContaDestinoId);

                if (contaDestino == null)
                {
                    return StatusCode(400, "Conta de destino não encontrada.");
                }

                if (depositoDto.Valor <= 0)
                {
                    return StatusCode(400, "O valor do depósito deve ser maior que zero.");
                }

                contaDestino.Saldo += depositoDto.Valor;

                var transacao = new Transacao
                {
                    DataHoraTrasacao = DateTime.Now,
                    TipoTransacao = "Deposito",
                    Valor = depositoDto.Valor,
                    ContaDestinoId = depositoDto.ContaDestinoId
                };

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
                var contaOrigem = _appDbContext.ContaCorrentes.FirstOrDefault(c => c.Id == saqueDto.ContaOrigemId);

                if (contaOrigem == null)
                {
                    return StatusCode(400, "Conta de origem não encontrada.");
                }

                if (saqueDto.Valor <= 0)
                {
                    return StatusCode(400, "O valor do saque deve ser maior que zero.");
                }

                if (contaOrigem.Saldo < saqueDto.Valor)
                {
                    return StatusCode(400, "Saldo insuficiente na conta de origem.");
                }

                contaOrigem.Saldo -= saqueDto.Valor;

                var transacao = new Transacao
                {
                    DataHoraTrasacao = DateTime.Now,
                    TipoTransacao = "Saque",
                    Valor = saqueDto.Valor,
                    ContaOrigemId = saqueDto.ContaOrigemId
                };

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
                var transacoes = _appDbContext.Transacaos.Where(t => t.ContaDestinoId == id).ToList();
                if (transacoes == null || transacoes.Count == 0)
                    return NotFound("Nenhuma transação encontrada com este ID");

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transações por ID: {ex.Message}");
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

        [HttpGet("conta/{contaId}")]
        public IActionResult ObterTransacoesPorContaId(int contaId)
        {
            try
            {
                var transacoes = _appDbContext.Transacaos
                    .Where(t => t.ContaOrigemId == contaId || t.ContaDestinoId == contaId)
                    .ToList();
                
                if (transacoes == null || transacoes.Count == 0)
                    return NotFound("Nenhuma transação encontrada para esta conta");

                return Ok(transacoes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transações por ID da conta: {ex.Message}");
            }
        }
    }
}