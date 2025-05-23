using FCG.Domain.Entities;
using FCG.Domain.Repository;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FCG.Infrastructure.Repository
{
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PromocaoRepository> _logger;

        public PromocaoRepository(AppDbContext context, ILogger<PromocaoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AlterarAsync(Promocao promocao)
        {
            try
            {
                _context.Promocoes.Update(promocao);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(AlterarAsync), ex.Message);
                return false;
            }
        }

        public async Task<Promocao> BuscarPorIdAsync(Guid id)
        {
            try
            {
                return await _context.Promocoes.FindAsync(id);
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
                var promocao = await _context.Promocoes.FindAsync(id);

                if (promocao != null)
                {
                    _context.Promocoes.Remove(promocao);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(DeletarAsync), ex.Message);
                return false;
            }

        }

        public async Task<IList<Promocao>> ListarAsync()
        {
            try
            {
                return await _context.Promocoes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(ListarAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> SalvarAsync(Promocao promocao)
        {
            try
            {
                await _context.Promocoes.AddAsync(promocao);
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