using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
{
    [ApiController]
    [Route("poupanca")]
    public class ContaPoupancaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        

        public ContaPoupancaController(AppDbContext appDbContext, IMapper mapper )
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
    
        }

        [HttpPost("{usuarioId:int}")]
public IActionResult CriarContaPoupanca(int usuarioId, [FromBody] CreateContaPoupancaDto novaContaPoupanca)
{
    // Verificar se o usuário existe
    var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
    if (usuario == null)
    {
        return NotFound("Usuário não encontrado.");
    }

    // Gerar um número de conta corrente único com seis dígitos
    string numeroContaPoupanca;
    do
    {
        numeroContaPoupanca = GerarNumeroContaPoupanca();
    } while (_appDbContext.ContaPoupancas.Any(cc => cc.NumeroConta == numeroContaPoupanca));

    Console.WriteLine($"Número da conta corrente gerado: {numeroContaPoupanca}");

    // Criar a conta corrente para o usuário
    var contaPoupanca = _mapper.Map<ContaPoupanca>(novaContaPoupanca);
    
    // Definir o UsuarioId e o número da conta corrente na nova conta corrente
    contaPoupanca.UsuarioId = usuarioId;
    contaPoupanca.NumeroConta = numeroContaPoupanca;
    contaPoupanca.TipoConta = Conta.Poupança;
    contaPoupanca.Rendimento = 0.05;
    contaPoupanca.Saldo = AtualizaSaldoComTaxaRendimento(contaPoupanca);
    // Adicionar a nova conta corrente ao contexto
    
     _appDbContext.ContaPoupancas.Add(contaPoupanca); 

    // Salvar as alterações no banco de dados
    _appDbContext.SaveChanges();

    string tableName = "transacoes" + contaPoupanca.NumeroConta;

      return CreatedAtAction(nameof(PegarPorId), new { id = contaPoupanca.Id }, contaPoupanca);
    }

private double AtualizaSaldoComTaxaRendimento(ContaPoupanca contaPoupanca) {
    double saldoInicial = contaPoupanca.Saldo; // Saldo inicial da conta
    double taxaRendimentoMensal = 0.05 / 12; // Taxa de rendimento mensal (5% ao ano dividido por 12 meses)
    int dias = 30; // Número de dias que queremos calcular

    // Calcula a taxa de rendimento diária com base na taxa mensal
    double taxaRendimentoDiaria = Math.Pow(1 + taxaRendimentoMensal, 1.0 / 30) - 1;

    // Calcula o saldo final após o período de dias especificado
    double saldoFinal = saldoInicial * Math.Pow(1 + taxaRendimentoDiaria, dias);

    saldoFinal = Math.Round(saldoFinal, 2);

    return saldoFinal;
}

    private string GerarNumeroContaPoupanca()
    {
    // Gerar um número de conta popanca com seis dígitos
    Random random = new Random();
    int numero = random.Next(100, 999); // Garante que o número tenha seis dígitos
    return numero.ToString();
    }

        [HttpGet("{id:int}")]
        public IActionResult PegarPorId(int id)
        {
            var conta = _appDbContext.ContaPoupancas.Find(id);

            if (conta == null)
                return NotFound();

            return Ok(conta);
        }

    }
}
