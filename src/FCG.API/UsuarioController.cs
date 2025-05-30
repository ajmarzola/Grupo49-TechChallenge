using FCG.API.Controllers;
using FCG.Application.Model;
using FCG.Application.Services;
using FCG.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using FCG.Application.DTOs;
using FCG.Application.Model.Extensions;

namespace FCG.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioService usuarioService, ILogger<UsuarioController> logger)
        { 
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UsuarioRegistroModel>>> Listar()
        {
            try
            {
  
                var usuarios = await _usuarioService.ListaUsuariosAsync();
                return Ok(usuarios.Convert());  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar Usuários.");
                return BadRequest("Erro ao listar usuários.");
            }
        }



        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UsuarioRegistroModel>> BuscarPorId(Guid id)
        {
            try
            {
                var usuario = await _usuarioService.BuscarUsuarioIdAsync(id);
                if (usuario == null)
                    return NotFound();

                return Ok(usuario.Convert());  
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar usuário por ID.");
                return BadRequest("Erro ao buscar usuário.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<UsuarioRegistroModel>> Salvar([FromBody] UsuarioRegistroModel jogo)
        {
            try
            {
                await _usuarioService.SalvarUsuarioAsync(jogo);
                return Ok(jogo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar Usuario.");
                return BadRequest("Erro ao cadastrar Usuario.");
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] UsuarioRegistroModel model)
        {
            try
            {
                var existente = await _usuarioService.BuscarUsuarioIdAsync(id);
                if (existente == null)
                    return NoContent();

                model.Id = id;
                var atualizado = await _usuarioService.AlterarAsync(model);
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar Usuario.");
                return BadRequest("Erro ao atualizar Usuario.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                var existente = await _usuarioService.BuscarUsuarioIdAsync(id);
                if (existente == null)
                    return NoContent();

                var resultado = await _usuarioService.DeletarUsuarioAsync(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover Usuario.");
                return BadRequest("Erro ao remover Usuario.");
            }
        }
    }
}
