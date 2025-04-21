using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using FCG.Application.Services;
using FCG.Application.Model;

namespace FCG.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _service;
        private readonly ILogger<AuthController> _logger;

        public JogosController(IJogoService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET /api/jogos
        [HttpGet]
        [Authorize] // qualquer usuário logado (Aluno ou Admin)
        public async Task<ActionResult<IEnumerable<JogoModel>>> Listar()
        {
            try
            {
                var jogos = await _service.ListarAsync();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar jogos");
                return BadRequest();
            }
        }

        // GET /api/jogos/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<JogoModel>> BuscarPorId(Guid id)
        {
            try
            {
                var jogo = await _service.BuscarPorIdAsync(id);

                if (jogo == null)
                {
                    return NoContent();
                }

                return Ok(jogo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar jogos");
                return BadRequest();
            }
        }

        // POST /api/jogos
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<JogoModel>> Salvar(JogoModel jogo)
        {
            try
            {
                await _service.SalvarAsync(jogo);
                return Ok(jogo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar jogos");
                return BadRequest();
            }
        }

        // PUT /api/jogos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutJogo(Guid id, JogoModel jogo)
        {
            try
            {
                var registro = await _service.BuscarPorIdAsync(id);

                if (registro == null)
                {
                    return NoContent();
                }

                jogo.Id = id;

                var retorno = await _service.AlterarAsync(jogo);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar jogos");
                return BadRequest();
            }
        }

        // DELETE /api/jogos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                var registro = await _service.BuscarPorIdAsync(id);

                if (registro == null)
                {
                    return NoContent();
                }

                var retorno = await _service.DeletarAsync(id);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao apagar jogos");
                return BadRequest();
            }
        }
    }
}