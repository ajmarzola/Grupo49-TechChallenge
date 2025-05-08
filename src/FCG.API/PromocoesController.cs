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
            //var promocao = await _context.Promocoes
            //    .Include(p => p.Jogo)
            //    .FirstOrDefaultAsync(p => p.Id == id);

            //if (promocao == null) return NotFound();
            //return promocao;
            return Ok();
        }

        // PUT: /api/promocoes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Promocao input)
        {
            //if (id != input.Id) return BadRequest();

            //_context.Entry(input).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
            //return NoContent();
            return Ok();
        }

        // DELETE: /api/promocoes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var promocao = await _context.Promocoes.FindAsync(id);
            //if (promocao == null) return NotFound();

            //_context.Promocoes.Remove(promocao);
            //await _context.SaveChangesAsync();
            //return NoContent();
            return Ok();
        }
    }
}
