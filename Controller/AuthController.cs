using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain;
using sharpcoder2_TechLife_Coinnecta_Backend.Domain.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace sharpcoder2_TechLife_Coinnecta_Backend.Controller
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthController(AppDbContext appDbContext, JwtSettings jwtSettings)
        {
            _appDbContext = appDbContext;
            _jwtSettings = jwtSettings;
        }

        [HttpPost("login")]
        [AllowAnonymous] // Permite que todos acessem este endpoint sem autenticação
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(x => x.Email == request.Email);
            var senha = usuario?.Senha;
            bool senhaCorreta = senha != null ? BCrypt.Net.BCrypt.Verify(request.Senha, senha) : false;

            if (!senhaCorreta)
                return BadRequest(new { message = "E-mail ou senha inválidos" });

            var usuarioId = usuario?.Id;

            var token = GenerateJwtToken(usuarioId.ToString());

            return Ok(new { token });
        }

        private string GenerateJwtToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }),
                Expires = DateTime.UtcNow.AddHours(1), // Definir tempo de expiração do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class JwtSettings
    {
        public string Secret { get; set; }
    }
}
