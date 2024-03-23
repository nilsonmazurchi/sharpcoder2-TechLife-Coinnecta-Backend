using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;



namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
{
    [ApiController]
    [Route("contas")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;


        public ContaCorrenteController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

        }
        [HttpPost("{usuarioId:int}")]
        public async Task<IActionResult> CriarContaCorrente(int usuarioId, [FromBody] CreateContaCorrenteDto novaContaCorrente)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var usuarioPossuiContaCorrente = _appDbContext.ContaCorrentes.Any(cc => cc.UsuarioId == usuarioId);
            if (usuarioPossuiContaCorrente)
            {
                return BadRequest("Usuário já possui uma conta corrente.");
            }

            string numeroContaCorrente;
            do
            {
                numeroContaCorrente = GerarNumeroContaCorrente();
            } while (_appDbContext.ContaCorrentes.Any(cc => cc.NumeroConta == numeroContaCorrente));

            Console.WriteLine($"Número da conta corrente gerado: {numeroContaCorrente}");

            var novaConta = _mapper.Map<ContaCorrente>(novaContaCorrente);

            novaConta.UsuarioId = usuarioId;
            novaConta.NumeroConta = numeroContaCorrente;
            novaConta.LimiteCredito = 100.00;
            novaConta.StatusConta = Conta.Ativo;
            novaConta.TipoConta = Conta.Corrente;
            novaConta.Saldo = 0.00;

            await _appDbContext.ContaCorrentes.AddAsync(novaConta);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(PegarPorId), new { id = novaConta.Id }, novaConta);
        }


        private string GerarNumeroContaCorrente()
        {
            // Gerar um número de conta corrente com seis dígitos
            Random random = new Random();
            int numero = random.Next(100000, 999999); // Garante que o número tenha seis dígitos
            return numero.ToString();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> PegarPorId(int id)
        {
            var conta = await _appDbContext.ContaCorrentes.FindAsync(id);

            if (conta == null)
                return NotFound();

            return Ok(conta);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> AtualizarContaCorrente(int id, [FromBody] UpdateContaCorrenteDto contaCorrenteAtualizada)
        {
            var contaCorrente = _appDbContext.ContaCorrentes.FirstOrDefault(cc => cc.Id == id);

            if (contaCorrente == null)
            {
                return NotFound("Conta corrente não encontrada.");
            }

            if (contaCorrenteAtualizada.TipoConta != null)
            {
                contaCorrente.TipoConta = contaCorrenteAtualizada.TipoConta;
            }

            if (contaCorrenteAtualizada.StatusConta != null)
            {
                contaCorrente.StatusConta = contaCorrenteAtualizada.StatusConta;
            }

            contaCorrente.Saldo = contaCorrenteAtualizada.Saldo;

            await _appDbContext.SaveChangesAsync();

            return Ok(contaCorrente);
        }
    }
}
