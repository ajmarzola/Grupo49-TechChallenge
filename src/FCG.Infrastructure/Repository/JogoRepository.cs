using FCG.Domain.Entities;
using FCG.Infrastructure.Data;
using FCG.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCG.Domain.Repository
{
    public class JogoRepository : IJogoRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CompraRepository> _logger;

        public JogoRepository(AppDbContext context, ILogger<CompraRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AlterarJogoAsync(Jogo model)
        {
            try
            {
                var jogo = await _context.Jogos.FindAsync(model.Id);

                if (jogo == null)
                {
                    return false;
                }

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(AlterarJogoAsync), ex.Message);
                throw;
            }
        }

        public async Task<Jogo> BuscarJogoIdAsync(Guid id)
        {
            try
            {
                return await _context.Jogos.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(BuscarJogoIdAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> DeletarJogoAsync(Guid id)
        {
            try
            {
                var jogo = await _context.Jogos.FindAsync(id);

                if (jogo == null)
                {
                    return false;
                }

                _context.Jogos.Remove(jogo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(DeletarJogoAsync), ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Jogo>> ListaJogoAsync()
        {
            try
            {
                return await _context.Jogos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(ListaJogoAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> SalvarJogoAsync(Jogo model)
        {
            try
            {
                model.Id = Guid.NewGuid();
                _context.Jogos.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(SalvarJogoAsync), ex.Message);
                throw;
            }
        }
    }
}