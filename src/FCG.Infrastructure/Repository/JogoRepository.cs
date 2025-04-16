using FCG.Domain.Entities;
using FCG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace FCG.Domain.Repository
{
    public class JogoRepository : IJogoRepository
    {
        private readonly AppDbContext _context;
        public JogoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AlterarJogo(Jogo model)
        {
            try
            {
                var jogo = await _context.Jogos.FindAsync(model.Id);
                if (jogo == null)
                    return false;

                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<Jogo> BucarJogo(Guid id)
        {
            try
            {
                return await _context.Jogos.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<bool> DeletarJogo(Guid id)
        {
            try
            {
                var jogo = await _context.Jogos.FindAsync(id);
                if (jogo == null) return false;

                _context.Jogos.Remove(jogo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<Jogo>> ListaJogo()
        {
            try
            {
                return await _context.Jogos.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<bool> SavarJogo(Jogo model)
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

                throw;
            }

        }
    }
}
