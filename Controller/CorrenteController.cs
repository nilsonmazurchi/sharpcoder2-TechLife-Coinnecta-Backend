// using AutoMapper;
// using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
// using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
// using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
// using Microsoft.AspNetCore.Mvc;


// namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
// {
//     [ApiController]
//     [Route("contascorrentes")]
//     public class PoupancaController : ControllerBase
//     {
//         private readonly AppDbContext _appDbContext;
//         private readonly IMapper _mapper;

//         public CorrenteController(AppDbContext appDbContext, IMapper mapper)
//         {
//             _appDbContext = appDbContext;
//             _mapper = mapper;
//         }

//         [HttpGet]
//         public ActionResult<IEnumerable<ContaCorrente>> PegarTodos()
//         {
//             return Ok(_appDbContext.ContaCorrentes.ToList());
//         }

//         [HttpGet("{id:int}")]
//         public IActionResult PegarPorId(int id)
//         {
//             var buscaContaCorrete = _appDbContext.ContaCorrentes.Find(id);

//             if (buscaContaCorrete == null)
//                 return NotFound();

//             return Ok(buscaContaCorrete);
//         }

//         [HttpPost]
//         public IActionResult Cadastrar(ContaCorrente contacorrente)
//         {
//             var result = _appDbContext.ContaCorrentes.Add(contacorrente);
//             _appDbContext.SaveChanges();
//             //var contaCorrenteSalva = result.Entity;
//             return Ok("Created!");
//             // status 201 + corpo vazio + header com redirecionamento
//             //return CreatedAtAction(nameof(PegarPorId), new { contaCorrenteSalva.Id }, contaCorrenteSalva);
//         }

//         [HttpGet("dataabertura")]
//         public string GetDataAbertura(string numeroconta)
//         {
            
//             var contacorrente = _appDbContext.ContaCorrentes.FirstOrDefault(u => u.NumeroConta == numeroconta);
//             return contacorrente?.DataAbertura;
             
//         }

//         [HttpGet("statusconta")]
//         public string GetStatusConta(StatusConta statusconta)
//         {
//             var contacorrente = _appDbContext.ContaCorrentes.FirstOrDefault(u => u.StatusConta == statusconta);
//             return contacorrente != null ? contacorrente.StatusConta : null;
//         }

//         [HttpGet("Balanco")]
//         public string GetBalanco(double balanco)
//         {
//             var contacorrente = _appDbContext.ContaCorrentes.FirstOrDefault(u => u.Balanco == balanco);
//             return contacorrente != null ?  contacorrente.Balanco : null;
//         }

//     }
// }
