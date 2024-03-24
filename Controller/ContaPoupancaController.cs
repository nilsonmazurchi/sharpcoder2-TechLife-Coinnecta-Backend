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


        public ContaPoupancaController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

        }

         [HttpPost("{usuarioId:int}")]
        public async Task<IActionResult> CriarContaPoupanca(int usuarioId, [FromBody] CreateContaPoupancaDto novaContaPoupanca)
        {
            var usuario = await _appDbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var usuarioPossuiContaPoupanca = await _appDbContext.ContaPoupancas.AnyAsync(cp => cp.UsuarioId == usuarioId);
            if (usuarioPossuiContaPoupanca)
            {
                return BadRequest("Usuário já possui uma conta poupança.");
            }

            string numeroContaPoupanca;
            do
            {
                numeroContaPoupanca = GerarNumeroContaPoupanca();
            } while (await _appDbContext.ContaPoupancas.AnyAsync(cc => cc.NumeroConta == numeroContaPoupanca));

            Console.WriteLine($"Número da conta poupança gerado: {numeroContaPoupanca}");

            var contaPoupanca = _mapper.Map<ContaPoupanca>(novaContaPoupanca);

            contaPoupanca.UsuarioId = usuarioId;
            contaPoupanca.NumeroConta = numeroContaPoupanca;
            contaPoupanca.TipoConta = Conta.Poupança;
            contaPoupanca.StatusConta = Conta.Ativo;

            _appDbContext.ContaPoupancas.Add(contaPoupanca);
            await _appDbContext.SaveChangesAsync();

            string tableName = "poupancaRendimento" + contaPoupanca.NumeroConta;
            string createTableSql = $@"
            CREATE TABLE IF NOT EXISTS {tableName} (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Dia INTEGER NOT NULL,
            Rendimento DOUBLE NOT NULL,
            ValorRendimento REAL NOT NULL,
            ValorFinal REAL NOT NULL,
            ContaPoupancaId INT NOT NULL,
            FOREIGN KEY (ContaPoupancaId) REFERENCES ContaPoupanca(Id)
            )";

            await _appDbContext.Database.ExecuteSqlRawAsync(createTableSql);

            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(PegarPorId), new { id = contaPoupanca.Id }, contaPoupanca);
        }


        private double AtualizaSaldoComTaxaRendimento(ContaPoupanca contaPoupanca)
        {
            double saldoInicial = contaPoupanca.Saldo; 
            double taxaRendimentoMensal = 0.05 / 12; 
            int dias = 30; 
            
            double taxaRendimentoDiaria = Math.Pow(1 + taxaRendimentoMensal, 1.0 / 30) - 1;
            
            double saldoFinal = saldoInicial * Math.Pow(1 + taxaRendimentoDiaria, dias);

            saldoFinal = Math.Round(saldoFinal, 2);

            return saldoFinal;
        }

        private string GerarNumeroContaPoupanca()
        {
           
            Random random = new Random();
            int numero = random.Next(100000000, 999999999); 
            return numero.ToString();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> PegarPorId(int id)
        {
            var conta = await _appDbContext.ContaPoupancas.FindAsync(id);
            if (conta == null)
                return NotFound();

            return Ok(conta);
        }
    }
}
