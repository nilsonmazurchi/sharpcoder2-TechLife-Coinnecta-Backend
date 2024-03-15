using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller;

[ApiController]
[Route("usuarios")]
public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UsuarioController(AppDbContext appDbContext, IMapper mapper) {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> PegarTodos() {
            return Ok(_appDbContext.Usuarios.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult PegarPorId(int id) {
            var buscaUsuario = _appDbContext.Usuarios.Find(id);

            if(buscaUsuario == null)
                return NotFound();
    
            return Ok(buscaUsuario);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]CreateUsuarioDto novoUsuario) {

            var usuarioParaCadastro = _mapper.Map<Usuario>(novoUsuario);

            var result = _appDbContext.Usuarios.Add(usuarioParaCadastro);
            _appDbContext.SaveChanges();    
            var usuarioSalvo = result.Entity;

            // status 201 + corpo vazio + header com redirecionamento
            return CreatedAtAction(nameof(PegarPorId), new { usuarioSalvo.Id }, usuarioSalvo);
        }
    }
