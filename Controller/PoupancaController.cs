// using AutoMapper;
// using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
// using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
// using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
// using Microsoft.AspNetCore.Mvc;


// namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
// {
//     [ApiController]
//     [Route("contaspoupanca")]
//     public class PoupancaController : ControllerBase
//     {
//         private readonly AppDbContext _appDbContext;
//         private readonly IMapper _mapper;

//         public PoupancaController(AppDbContext appDbContext, IMapper mapper)
//         {
//             _appDbContext = appDbContext;
//             _mapper = mapper;
//         }

//         [HttpGet]
//         public ActionResult<IEnumerable<ContaPoupanca>> PegarTodos()
//         {
//             return Ok(_appDbContext.ContaPoupancas.ToList());
//         }

//         [HttpGet("{id:int}")]
//         public IActionResult PegarPorId(int id)
//         {
//             var buscaContaPoupanca = _appDbContext.ContaPoupancas.Find(id);

//             if (buscaContaPoupanca == null)
//                 return NotFound();

//             return Ok(buscaContaPoupanca);
//         }

//         [HttpPost]
//         public IActionResult Cadastrar(ContaPoupanca contapoupanca)
//         {
//             var result = _appDbContext.ContaPoupancas.Add(contapoupanca);
//             _appDbContext.SaveChanges();
//             //var contaPoupancaSalva = result.Entity;
//             return Ok("Created!");
//             // status 201 + corpo vazio + header com redirecionamento
//             //return CreatedAtAction(nameof(PegarPorId), new { contaPoupancaSalva.Id }, contaPoupancaSalva);
//         }

//         [HttpGet("dataabertura")]
//         public string GetDataAbertura(string numeroconta)
//         {
            
//             var contapoupanca = _appDbContext.ContaPoupancas.FirstOrDefault(u => u.NumeroConta == numeroconta);
//             return contapoupanca?.DataAbertura;
             
//         }

//         [HttpGet("statusconta")]
//         public string GetStatusConta(StatusConta statusconta)
//         {
//             var contapoupanca = _appDbContext.ContaPoupancas.FirstOrDefault(u => u.StatusConta == statusconta);
//             return contapoupanca != null ? contapoupanca.StatusConta : null;
//         }

//         [HttpGet("Balanco")]
//         public string GetBalanco(double balanco)
//         {
//             var contapoupanca = _appDbContext.ContaPoupancas.FirstOrDefault(u => u.Balanco == balanco);
//             return contapoupanca != null ? contapoupanca.Balanco : null;
//         }

//     }
// }
