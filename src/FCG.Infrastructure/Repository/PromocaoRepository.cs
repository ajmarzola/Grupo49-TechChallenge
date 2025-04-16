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
    public class PromocaoRepository : IPromocaoRepository
    {
        private readonly AppDbContext _context;

        public PromocaoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AterarPromocaoAsync(Promocao promocao)
        {
            try
            {
                _context.Promocoes.Update(promocao);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<Promocao> BuscarPromocaoIdAsync(Guid id)
        {
            try
            {
                return await _context.Promocoes.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<bool> DeletarPromocaoAsync(Guid id)
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
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<IEnumerable<Promocao>> ListaPromocaoAsync()
        {
            try
            {
                return await _context.Promocoes.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> SalvarPromocaoAsync(Promocao promocao)
        {
            try
            {
                await _context.Promocoes.AddAsync(promocao);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
