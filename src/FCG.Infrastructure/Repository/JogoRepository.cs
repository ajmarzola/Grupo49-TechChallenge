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

        public async Task<bool> AlterarAsync(Jogo model)
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
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(AlterarAsync), ex.Message);
                throw;
            }
        }

        public async Task<Jogo> BuscarPorIdAsync(Guid id)
        {
            try
            {
                return await _context.Jogos.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(BuscarPorIdAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> DeletarAsync(Guid id)
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
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(DeletarAsync), ex.Message);
                throw;
            }
        }

        public async Task<IList<Jogo>> ListarAsync()
        {
            try
            {
                return await _context.Jogos.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(ListarAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> SalvarAsync(Jogo model)
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
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(SalvarAsync), ex.Message);
                throw;
            }
        }
    }
}