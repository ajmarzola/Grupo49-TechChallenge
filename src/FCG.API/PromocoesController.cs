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

namespace FCG.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class PromocoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PromocoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/promocoes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Promocao>>> Get()
        {
            var hoje = DateTime.UtcNow;
            return await _context.Promocoes
                .Where(p => p.DataInicio <= hoje && p.DataFim >= hoje)
                .Include(p => p.Jogo)
                .ToListAsync();
        }

        // POST: /api/promocoes
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Promocao>> Post([FromBody] Promocao promocao)
        {
            promocao.Id = Guid.NewGuid();
            _context.Promocoes.Add(promocao);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = promocao.Id }, promocao);
        }

        // GET: /api/promocoes/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Promocao>> GetById(Guid id)
        {
            var promocao = await _context.Promocoes
                .Include(p => p.Jogo)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (promocao == null) return NotFound();
            return promocao;
        }

        // PUT: /api/promocoes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Promocao input)
        {
            if (id != input.Id) return BadRequest();

            _context.Entry(input).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: /api/promocoes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var promocao = await _context.Promocoes.FindAsync(id);
            if (promocao == null) return NotFound();

            _context.Promocoes.Remove(promocao);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
