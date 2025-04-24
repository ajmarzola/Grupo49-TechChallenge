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
    public class CompraRepository : ICompraRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CompraRepository> _logger;

        public CompraRepository(AppDbContext context, ILogger<CompraRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Compra> BuscarCompraPorIdAsync(Guid id)
        {
            try
            {
                return await _context.Compras.FirstOrDefaultAsync(f => f.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(BuscarCompraPorIdAsync), ex.Message);
                throw;
            }
        }

        public async Task<IList<Compra>> ListarCompraAsync()
        {
            try
            {
                return await _context.Compras.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(ListarCompraAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> SalvarCompraAsync(Compra compra)
        {
            try
            {
                await _context.Compras.AddAsync(compra);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(SalvarCompraAsync), ex.Message);
                return false;
            }
        }

        public async Task<bool> AlterarCompraAsync(Compra compra)
        {
            try
            {
                _context.Compras.Update(compra);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(AlterarCompraAsync), ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletarCompraAsync(Guid id)
        {
            try
            {
                var compra = await _context.Compras.FindAsync(id);

                if (compra != null)
                {
                    _context.Compras.Remove(compra);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(DeletarCompraAsync), ex.Message);
                return false;
            }
        }
    }
}