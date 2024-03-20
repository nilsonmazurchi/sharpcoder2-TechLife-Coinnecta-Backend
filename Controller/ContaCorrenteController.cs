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
    [Route("contas")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        

        public ContaCorrenteController(AppDbContext appDbContext, IMapper mapper )
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
    
        }

        [HttpPost("{usuarioId:int}")]
public IActionResult CriarContaCorrente(int usuarioId, [FromBody] CreateContaCorrenteDto novaContaCorrente)
{
    // Verificar se o usuário existe
    var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
    if (usuario == null)
    {
        return NotFound("Usuário não encontrado.");
    }

    // Gerar um número de conta corrente único com seis dígitos
    string numeroContaCorrente;
    do
    {
        numeroContaCorrente = GerarNumeroContaCorrente();
    } while (_appDbContext.ContaCorrentes.Any(cc => cc.NumeroConta == numeroContaCorrente));

    Console.WriteLine($"Número da conta corrente gerado: {numeroContaCorrente}");

    // Criar a conta corrente para o usuário
    var novaConta = _mapper.Map<ContaCorrente>(novaContaCorrente);
    
    // Definir o UsuarioId e o número da conta corrente na nova conta corrente
    novaConta.UsuarioId = usuarioId;
    novaConta.NumeroConta = numeroContaCorrente;

    // Adicionar a nova conta corrente ao contexto
    var result = _appDbContext.ContaCorrentes.Add(novaConta); 

    // Salvar as alterações no banco de dados
    _appDbContext.SaveChanges();

    string tableName = "transacoes" + novaConta.NumeroConta;

   string createTableSql = $@"
CREATE TABLE IF NOT EXISTS {tableName} (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    DataRealizacao DATETIME NOT NULL,
    Descricao VARCHAR(255),
    TipoTransacao INTEGER NOT NULL,
    ValorTransacao REAL NOT NULL,
    Saldo REAL NOT NULL,
    ContaCorrenteId INT NOT NULL,
    FOREIGN KEY (ContaCorrenteId) REFERENCES ContaCorrente(Id)
)";

_appDbContext.Database.ExecuteSqlRaw(createTableSql);

    _appDbContext.SaveChanges();


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
        public IActionResult PegarPorId(int id)
        {
            var conta = _appDbContext.ContaCorrentes.Find(id);

            if (conta == null)
                return NotFound();

            return Ok(conta);
        }

    }
}
