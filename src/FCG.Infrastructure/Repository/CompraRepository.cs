using FCG.Domain.Entities;
using FCG.Domain.Repository;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Infrastructure.Repository
{
    public class CompraRepository : ICompraRepository
    {
        private readonly AppDbContext _context;

        public CompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Compra> BuscarCompraIdAsync(Guid id)
        {
            try
            {
                return await _context.Compras.FirstOrDefaultAsync(f => f.Id == id);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<IEnumerable<Compra>> ListaCompraAsync()
        {
            try
            {
                return await _context.Compras.ToListAsync();
            }
            catch (Exception ex)
            {

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

                throw;
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

                throw;
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

                throw;
            }

        }
    }
}
