using FCG.Domain.Entities;
using FCG.Infrastructure.Data;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FCG.API.GraphQL
{
    public class Queries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Jogo> GetJogos([Service] AppDbContext context) => context.Jogos;

        [UseProjection]
        [UseFiltering]
        public IQueryable<Promocao> GetPromocoesAtivas([Service] AppDbContext context)
        {
            var hoje = DateTime.UtcNow;
            return context.Promocoes.Where(p => p.DataInicio <= hoje && p.DataFim >= hoje);
        }

        [Authorize(Roles = "Aluno" )]
        [UseProjection]
        public IQueryable<Compra> GetMinhasCompras([Service] AppDbContext context, ClaimsPrincipal user)
        {
            var userId = user.FindFirst("UserId")?.Value;
            if (userId == null) return Enumerable.Empty<Compra>().AsQueryable();
            return context.Compras.Where(c => c.UsuarioId.ToString() == userId);
        }
    }
}
