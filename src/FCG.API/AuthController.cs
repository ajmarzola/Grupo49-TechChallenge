using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using FCG.Application.DTOs;
using Microsoft.Extensions.Logging;
using FCG.Application.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FCG.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IUsuarioService _usuarioService;

        public AuthController(IConfiguration configuration,
            ILogger<AuthController> logger, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroModel dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var _usuario = await _usuarioService.BuscarUsuarioEmailAsync(dto.Email);
                if (_usuario != null)
                    return BadRequest("Usuário já cadastrado.");

                if (!await _usuarioService.SalvarUsuarioAsync(dto))
                    return BadRequest("Não foi possível cadastrar Usuário.");

                return Ok("Usuário registrado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar o usuário: {Nome} com email: {Email}", dto.Nome, dto.Email);
                return BadRequest("Não foi possível cadastrar Usuário.");
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginModel dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuario = await _usuarioService.BuscarUsuarioEmailAsync(dto.Email);
                if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha))
                    return Unauthorized("Credenciais inválidas.");

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]!);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Role),
                new Claim("UserId", usuario.Id.ToString())
            }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro ao registrar o usuário com email: {Email}", dto.Email);
                return BadRequest();
            }

        }

        [HttpDelete("DeletarUsuario")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeletarUsuario([FromBody] Guid id)
        {
            try
            {
                var usuario = await _usuarioService.BuscarUsuarioIdAsync(id);
                if (usuario == null)
                    return Unauthorized("Credenciais inválidas.");

                if (!await _usuarioService.DeletarUsuarioAsync(id))
                    return BadRequest("Não foi possível deletar Usuário.");

                return Ok("Usuário deletado com sucesso.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro ao registrar o usuário com id: {id}", id);
                return BadRequest();
            }

        }

        [HttpPut("AlteraUsuario")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AlteraUsuario([FromBody] UsuarioRegistroModel dto)
        {
            try
            {
                var usuario = await _usuarioService.BuscarUsuarioEmailAsync(dto.Email);
                if (usuario == null)
                    return Unauthorized("Credenciais inválidas.");

                if (!await _usuarioService.AlterarAsync(dto))
                    return BadRequest("Não foi possível alterar Usuário.");

                return Ok("Usuário alterado com sucesso.");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Erro ao registrar o usuário com email: {Email}", dto.Email);
                return BadRequest();
            }

        }
    }
}