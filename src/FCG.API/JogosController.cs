using FCG.Domain.Entities;
using FCG.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FCG.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class JogosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JogosController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/jogos
        [HttpGet]
        [Authorize] // qualquer usuário logado (Aluno ou Admin)
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogos()
        {
            return await _context.Jogos.ToListAsync();
        }

        // GET /api/jogos/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Jogo>> GetJogo(Guid id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();
            return jogo;
        }

        // POST /api/jogos
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Jogo>> PostJogo(Jogo jogo)
        {
            jogo.Id = Guid.NewGuid();
            _context.Jogos.Add(jogo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJogo), new { id = jogo.Id }, jogo);
        }

        // PUT /api/jogos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutJogo(Guid id, Jogo jogo)
        {
            if (id != jogo.Id) return BadRequest();

            _context.Entry(jogo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/jogos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteJogo(Guid id)
        {
            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null) return NotFound();

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
