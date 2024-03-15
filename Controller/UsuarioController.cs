using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;


namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UsuarioController(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> PegarTodos()
        {
            return Ok(_appDbContext.Usuarios.ToList());
        }

        [HttpGet("{id:int}")]
        public IActionResult PegarPorId(int id)
        {
            var buscaUsuario = _appDbContext.Usuarios.Find(id);

            if (buscaUsuario == null)
                return NotFound();

            return Ok(buscaUsuario);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]CreateUsuarioDto novoUsuario)
        {
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);
            novoUsuario.Senha = senhaCriptografada;
            var usuarioParaCadastro = _mapper.Map<Usuario>(novoUsuario);

            var result = _appDbContext.Usuarios.Add(usuarioParaCadastro);
            _appDbContext.SaveChanges();
            var usuarioSalvo = result.Entity;

            // status 201 + corpo vazio + header com redirecionamento
            return CreatedAtAction(nameof(PegarPorId), new { usuarioSalvo.Id }, usuarioSalvo);
        }

        [HttpGet("nome")]
        public string GetNome(string cpf)
        {
            #pragma warning disable 
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cpf == cpf);
            return usuario?.Nome;
             
        }

        [HttpGet("senha")]
        public string GetSenha(string cpf)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cpf == cpf);
            return usuario != null ? usuario.Senha : null;
        }

        [HttpGet("senha-cnpj")]
        public string GetSenhaCNPJ(string cnpj)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cnpj == cnpj);
            return usuario != null ? usuario.Senha : null;
        }

        [HttpGet("nome-cnpj")]
        public string GetNomeCNPJ(string cnpj)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cnpj == cnpj);
            return usuario?.Nome;
        }

        // [HttpGet("usuario-logado")]
        // public IActionResult GetUsuarioLogado(string cpf)
        // {
        //     // Lógica para obter o usuário logado
        //     // Se estiver autenticado, retorne o usuário logado, caso contrário, retorne null
        // }

     [HttpGet("checar-email")]
public bool ChecarEmailUsuarioExiste(string email)
{
    return _appDbContext.Usuarios.Any(u => u.Email == email);
}

        [HttpGet("checar-cnpj")]
        public bool ChecarCNPJUsuarioExiste(string cnpj)
        {
            return _appDbContext.Usuarios.Any(u => u.Cnpj == cnpj);
        }

        [HttpGet("checar-cpf")]
        public bool ChecarCPFUsuarioExiste(string cpf)
        {
            return _appDbContext.Usuarios.Any(u => u.Cpf == cpf);
        }
    }
}
