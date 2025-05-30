using FCG.Application.DTOs;
using FCG.Domain.Entities;
using FCG.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger )
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
        public async Task<bool> AlterarAsync(UsuarioRegistroModel usuarioModel)
        {
            try
            {
                var _usuario = await BuscarUsuarioIdAsync(usuarioModel.Id);
                if (_usuario == null)
                    return false;

                var usuario = new Usuario
                {
                    Nome = usuarioModel.Nome,
                    Email = usuarioModel.Email,
                    Senha = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha),
                    Role = usuarioModel.Role
                };
                return await _usuarioRepository.AlterarAsync(usuario);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(AlterarAsync), ex.Message);
                throw;
            }
        }

        public Task<Usuario> BuscarUsuarioEmailAsync(string email)
        {
            try
            {
                return _usuarioRepository.BuscarUsuarioEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(BuscarUsuarioEmailAsync), ex.Message);
                throw;
            }
        }

        public async Task<Usuario> BuscarUsuarioIdAsync(Guid id)
        {
            try
            {
                return await _usuarioRepository.BuscarUsuarioIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(BuscarUsuarioIdAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> DeletarUsuarioAsync(Guid id)
        {
            try
            {
                var usuario = await BuscarUsuarioIdAsync(id);
                if (usuario == null)
                    return false;

                return await _usuarioRepository.DeletarUsuarioAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(DeletarUsuarioAsync), ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Usuario>> ListaUsuariosAsync()
        {
            try
            {
                return await _usuarioRepository.ListaUsuariosAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(ListaUsuariosAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> SalvarUsuarioAsync(UsuarioRegistroModel usuarioModel)
        {
            try
            {
                var _usuario = await BuscarUsuarioEmailAsync(usuarioModel.Email);
                if (_usuario != null)
                    return false;

                var usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = usuarioModel.Nome,
                    Email = usuarioModel.Email,
                    Senha = BCrypt.Net.BCrypt.HashPassword(usuarioModel.Senha),
                    Role = usuarioModel.Role
                };
                return await _usuarioRepository.SalvarUsuarioAsync(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(SalvarUsuarioAsync), ex.Message);
                throw;
            }
        }
    }
}
