using FCG.Domain.Entities;
using FCG.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FCG.API
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Aluno")]
    public class ComprasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComprasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /api/compras/minhas
        [HttpGet("minhas")]
        public async Task<ActionResult<IEnumerable<object>>> MinhasCompras()
        {
            var userId = User.FindFirstValue("UserId");

            var compras = await _context.Compras
                .Include(c => c.Jogo)
                .Where(c => c.UsuarioId.ToString() == userId)
                .Select(c => new
                {
                    c.Id,
                    c.DataCompra,
                    c.ValorPago,
                    Jogo = new { c.Jogo!.Nome, c.Jogo.Descricao }
                })
                .ToListAsync();

            return Ok(compras);
        }

        // POST: /api/compras/comprar/{jogoId}
        [HttpPost("comprar/{jogoId}")]
        public async Task<IActionResult> ComprarJogo(Guid jogoId)
        {
            var userId = User.FindFirstValue("UserId");
            var usuario = await _context.Usuarios.FindAsync(Guid.Parse(userId!));
            if (usuario == null) return Unauthorized();

            var jogo = await _context.Jogos.Include(j => j.Promocoes).FirstOrDefaultAsync(j => j.Id == jogoId);
            if (jogo == null) return NotFound("Jogo não encontrado.");

            // Aplica desconto (se houver promoção ativa)
            var agora = DateTime.UtcNow;
            var promocaoAtiva = jogo.Promocoes.FirstOrDefault(p => p.DataInicio <= agora && p.DataFim >= agora);
            var desconto = promocaoAtiva != null ? promocaoAtiva.DescontoPercentual : 0;
            var valorFinal = jogo.Preco * (1 - (desconto / 100));

            var compra = new Compra
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuario.Id,
                JogoId = jogo.Id,
                DataCompra = DateTime.UtcNow,
                ValorPago = valorFinal
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Mensagem = "Compra realizada com sucesso.",
                ValorPago = valorFinal,
                PromocaoAplicada = desconto > 0 ? $"{desconto}% OFF" : "Sem promoção"
            });
        }
    }
}
