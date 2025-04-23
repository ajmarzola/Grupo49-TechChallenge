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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(AppDbContext context, ILogger<UsuarioRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AlterarAsync(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(AlterarAsync), ex.Message);
                return false;
            }
        }

        public async Task<Usuario> BuscarUsuarioIdAsync(Guid id)
        {
            try
            {
                return await _context.Usuarios.FirstAsync(x => x.Id == id);
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
                var usuario = await _context.Promocoes.FindAsync(id);

                if (usuario != null)
                {
                    _context.Promocoes.Remove(usuario);
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

        public async Task<IList<Usuario>> ListarAsync()
        {
            try
            {
                return await _context.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(ListarAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> SalvarAsync(Usuario usuario)
        {
            try
            {
                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(SalvarAsync), ex.Message);
                return false;
            }
        }
    }
}