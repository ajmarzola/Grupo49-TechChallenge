using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using FCG.Application.Services;
using FCG.Application.Model;

namespace FCG.API.Controllers
{
    /// <summary>
    /// Controller responsável pelas operações de gerenciamento de jogos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;
        private readonly ILogger<JogosController> _logger;

        /// <summary>
        /// Construtor do controller de jogos.
        /// </summary>
        /// <param name="jogoService">Serviço de aplicação para jogos.</param>
        /// <param name="logger">Logger para rastreamento de erros.</param>
        public JogosController(IJogoService jogoService, ILogger<JogosController> logger)
        {
            _jogoService = jogoService;
            _logger = logger;
        }

        /// <summary>
        /// Retorna a lista de todos os jogos cadastrados.
        /// </summary>
        /// <returns>Lista de jogos.</returns>
        /// <response code="200">Retorna a lista de jogos.</response>
        /// <response code="400">Erro ao buscar os jogos.</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<JogoModel>>> Listar()
        {
            try
            {
                var jogos = await _jogoService.ListarAsync();
                return Ok(jogos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar jogos.");
                return BadRequest("Erro ao listar jogos.");
            }
        }

        /// <summary>
        /// Retorna os dados de um jogo específico.
        /// </summary>
        /// <param name="id">Identificador único do jogo.</param>
        /// <returns>Dados do jogo.</returns>
        /// <response code="200">Jogo encontrado.</response>
        /// <response code="204">Jogo não encontrado.</response>
        /// <response code="400">Erro ao buscar o jogo.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<JogoModel>> BuscarPorId(Guid id)
        {
            try
            {
                var jogo = await _jogoService.BuscarPorIdAsync(id);
                return jogo != null ? Ok(jogo) : NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar jogo por ID.");
                return BadRequest("Erro ao buscar jogo.");
            }
        }

        /// <summary>
        /// Cadastra um novo jogo.
        /// </summary>
        /// <param name="jogo">Dados do jogo a ser cadastrado.</param>
        /// <returns>Jogo cadastrado.</returns>
        /// <response code="200">Jogo cadastrado com sucesso.</response>
        /// <response code="400">Erro ao cadastrar o jogo.</response>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<JogoModel>> Salvar([FromBody] JogoModel jogo)
        {
            try
            {
                return Ok(await _jogoService.SalvarAsync(jogo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar jogo.");
                return BadRequest("Erro ao cadastrar jogo.");
            }
        }

        /// <summary>
        /// Atualiza os dados de um jogo existente.
        /// </summary>
        /// <param name="id">Identificador do jogo a ser alterado.</param>
        /// <param name="jogo">Novos dados do jogo.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="200">Jogo atualizado com sucesso.</response>
        /// <response code="204">Jogo não encontrado.</response>
        /// <response code="400">Erro ao atualizar o jogo.</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Alterar(Guid id, [FromBody] JogoModel jogo)
        {
            try
            {
                var existente = await _jogoService.BuscarPorIdAsync(id);
                if (existente == null)
                    return NoContent();

                jogo.Id = id;
                var atualizado = await _jogoService.AlterarAsync(jogo);
                return Ok(atualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar jogo.");
                return BadRequest("Erro ao atualizar jogo.");
            }
        }

        /// <summary>
        /// Remove um jogo existente.
        /// </summary>
        /// <param name="id">Identificador do jogo a ser removido.</param>
        /// <returns>Status da operação.</returns>
        /// <response code="200">Jogo removido com sucesso.</response>
        /// <response code="204">Jogo não encontrado.</response>
        /// <response code="400">Erro ao remover o jogo.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                var existente = await _jogoService.BuscarPorIdAsync(id);
                if (existente == null)
                    return NoContent();

                var resultado = await _jogoService.DeletarAsync(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover jogo.");
                return BadRequest("Erro ao remover jogo.");
            }
        }
    }
}