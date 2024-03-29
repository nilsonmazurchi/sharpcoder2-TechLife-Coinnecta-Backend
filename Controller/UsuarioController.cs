using AutoMapper;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos.Usuario;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


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
        [Authorize]
        public async Task<ActionResult> PegarTodos()
        {
            return Ok(await _appDbContext.Usuarios.ToListAsync());
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> PegarPorId(int id)
        {
            var buscaUsuario = await _appDbContext.Usuarios.FindAsync(id);

            if (buscaUsuario == null)
                return NotFound();

            return Ok(buscaUsuario);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar([FromBody] CreateUsuarioDto novoUsuario)
        {
            var senhaCriptografada = BCrypt.Net.BCrypt.HashPassword(novoUsuario.Senha);
            novoUsuario.Senha = senhaCriptografada;
            var usuarioParaCadastro = _mapper.Map<Usuario>(novoUsuario);

            usuarioParaCadastro.StatusUsuario = Usuario.Ativo;

            var result = await _appDbContext.Usuarios.AddAsync(usuarioParaCadastro);
            await _appDbContext.SaveChangesAsync();
            var usuarioSalvo = result.Entity;

            return CreatedAtAction(nameof(PegarPorId), new { usuarioSalvo.Id }, usuarioSalvo);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(int id, [FromBody] UpdateUsuarioDto usuarioAtualizado)
        {
            if (id <= 0)
            {
                return BadRequest("ID de usuário inválido.");
            }

            var usuarioExistente = await _appDbContext.Usuarios.FindAsync(id);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            usuarioExistente.Nome = usuarioAtualizado.Nome;
            usuarioExistente.Ddd = usuarioAtualizado.Ddd;
            usuarioExistente.Telefone = usuarioAtualizado.Telefone;
            usuarioExistente.Email = usuarioAtualizado.Email;
            usuarioExistente.Cpf = usuarioAtualizado.Cpf;
            usuarioExistente.Cnpj = usuarioAtualizado.Cnpj;
            usuarioExistente.DiaNascimento = usuarioAtualizado.DiaNascimento;
            usuarioExistente.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioAtualizado.Senha);
            usuarioExistente.TipoPessoa = usuarioAtualizado.TipoPessoa;
            usuarioExistente.StatusUsuario = usuarioAtualizado.StatusUsuario;

            _appDbContext.Usuarios.Update(usuarioExistente);
            await _appDbContext.SaveChangesAsync();

            return Ok("Usuário atualizado com sucesso.");
        }

        [HttpGet("nome")]
        [AllowAnonymous]
        public IActionResult GetNome(string cpf)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cpf == cpf);

            if (usuario == null)
                return NotFound();

            return Ok(new { Nome = usuario.Nome });
        }

        [HttpGet("senha")]
        [Authorize]
        public string GetSenha(string cpf)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cpf == cpf);
            return usuario != null ? usuario.Senha : null;
        }

        [HttpGet("senha-cnpj")]
        [Authorize]
        public string GetSenhaCNPJ(string cnpj)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cnpj == cnpj);
            return usuario != null ? usuario.Senha : null;
        }

        [HttpGet("nome-cnpj")]
        [Authorize]
        public string GetNomeCNPJ(string cnpj)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(u => u.Cnpj == cnpj);
            return usuario?.Nome;
        }

        [HttpGet("checar-email")]
        [Authorize]
        public bool ChecarEmailUsuarioExiste(string email)
        {
            return _appDbContext.Usuarios.Any(u => u.Email == email);
        }

        [HttpGet("checar-cnpj")]
        [Authorize]
        public bool ChecarCNPJUsuarioExiste(string cnpj)
        {
            return _appDbContext.Usuarios.Any(u => u.Cnpj == cnpj);
        }

        [HttpGet("checar-cpf")]
        [AllowAnonymous]
        public bool ChecarCPFUsuarioExiste(string cpf)
        {
            return _appDbContext.Usuarios.Any(u => u.Cpf == cpf);
        }
    }
}
