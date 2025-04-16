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
        

        public JogosController()
        {
          
        }

        // GET /api/jogos
        [HttpGet]
        [Authorize] // qualquer usuário logado (Aluno ou Admin)
        public async Task<ActionResult<IEnumerable<Jogo>>> GetJogos()
        {
            return NoContent();
        }

        // GET /api/jogos/{id}
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Jogo>> GetJogo(Guid id)
        {
            return NoContent();
        }

        // POST /api/jogos
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<Jogo>> PostJogo(Jogo jogo)
        {
            return NoContent();
        }

        // PUT /api/jogos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutJogo(Guid id, Jogo jogo)
        {
            return NoContent();
        }

        // DELETE /api/jogos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteJogo(Guid id)
        {

            return NoContent();
        }
    }
}
