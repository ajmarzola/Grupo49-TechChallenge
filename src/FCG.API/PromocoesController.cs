using FCG.Domain.Entities;
using FCG.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using FCG.API.Controllers;
using FCG.Application.Services;
using Microsoft.Extensions.Logging;
using FCG.Application.Model;
using System.Reflection;

namespace FCG.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromocoesController : ControllerBase
    {
        private readonly IPromocaoService _promocaoService;
        private readonly ILogger<PromocoesController> _logger;

        public PromocoesController(IPromocaoService promocaoService, ILogger<PromocoesController> logger)
        {
            _promocaoService = promocaoService;
            _logger = logger;
        }

        // GET: /api/promocoes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Promocao>>> Get()
        {
            try
            {
                var promocoes = await _promocaoService.ListarAsync();
                return Ok(promocoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar promocoes.");
                return BadRequest("Erro ao listar promocoes.");
            }
        }

        // POST: /api/promocoes
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Promocao>> Post([FromBody] PromocaoModel promocao)
        {
            promocao.Id = Guid.NewGuid();

            try
            {
                var RegisterPromocoes = await _promocaoService.SalvarAsync(promocao);

                if (RegisterPromocoes)
                {
                    return StatusCode(StatusCodes.Status200OK, "Promoção cadastrada com sucesso");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Ops, alguma coisa deu errada ao cadastrar promoção");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar promocao.");
                return BadRequest("Erro ao cadastrar promocao.");
            }
        }

        // GET: /api/promocoes/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Promocao>> GetById(Guid id)
        {

            var promocao = await _promocaoService.BuscarPorIdAsync(id);
            return Ok(promocao);
        }

        // PUT: /api/promocoes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PromocaoModel input)
        {
            try
            {
                if (id != input.Id)
                    return BadRequest("O ID da URL não corresponde ao ID do corpo da requisição.");

                var existing = await _promocaoService.BuscarPorIdAsync(id);
                if (existing == null)
                    return NotFound();

                return Ok(await _promocaoService.AlterarAsync(input));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao alterar promocoes.");
                return BadRequest("Erro ao alterar promocoes.");
            }
        }

        // DELETE: /api/promocoes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var existing = await _promocaoService.BuscarPorIdAsync(id);
                if (existing == null)
                    return NotFound();

                return Ok(await _promocaoService.DeletarAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir promocoes.");
                return BadRequest("Erro ao excluir promocoes.");
            }
        }
    }
}
