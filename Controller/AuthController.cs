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
        // private readonly JwtSettings _jwtSettings;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }


        [HttpPost("login")]
        [AllowAnonymous] // Permite que todos acessem este endpoint sem autenticação
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var usuario = _appDbContext.Usuarios.FirstOrDefault(x => x.Cpf == request.CPF);
            var senha = usuario?.Senha;
            bool senhaCorreta = senha != null ? BCrypt.Net.BCrypt.Verify(request.Senha, senha) : false;

            if (!senhaCorreta)
                return BadRequest(new { message = "CPF ou senha inválidos" });

            var usuarioId = usuario?.Id;

            var token = GenerateToken(usuarioId.ToString());

            return Ok(new { token });
        }

        private string GenerateToken(string userId)
        {
            var claims = new[]
            {
                new Claim("id", userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretkey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);
        }

        // private string GenerateJwtToken(string userId)
        // {
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        //     var tokenDescriptor = new SecurityTokenDescriptor
        //     {
        //         Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }),
        //         Expires = DateTime.UtcNow.AddHours(1), // Definir tempo de expiração do token
        //         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //     };
        //     var token = tokenHandler.CreateToken(tokenDescriptor);
        //     return tokenHandler.WriteToken(token);
        // }
    }

    // public class JwtSettings
    // {
    //     public string Secret { get; set; }
    // }
}
