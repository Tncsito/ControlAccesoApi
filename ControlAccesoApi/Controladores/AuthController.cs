using ControlAccesoApi.Modelos;
using ControlAccesoApi.Repositorios; // Asegúrate de tener el namespace correcto para tu repositorio
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ControlAccesoApi.Controladores
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public AuthController(IConfiguration config, UsuarioRepositorio usuarioRepositorio)
        {
            _config = config;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _usuarioRepositorio.ObtenerUsuarioPorCorreo(model.Correo);
            if (user != null && model.Clave == user.Clave) // Reemplázalo con validación real
            {
                var token = GenerateJwtToken(model.Correo);
                return Ok(new { token });
            }
            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}