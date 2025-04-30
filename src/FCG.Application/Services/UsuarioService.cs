using FCG.Application.DTOs;
using FCG.Domain.Entities;
using FCG.Domain.Repository;
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
        private readonly ILogger<UsuarioService> _logger;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(ILogger<UsuarioService> logger, IUsuarioRepository usuarioRepository)
        { 
            _logger = logger;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<bool> AlterarUsuarioAsync(UsuarioRegistroModel model)
        {
            try
            {
                var _usuario = await _usuarioRepository.BuscarUsuarioEmailAsync(model.Email);
                if (_usuario == null) 
                    return false;

                var usuario = await ConverteModelParaEntidade(model);
                return await _usuarioRepository.AlterarUsuarioAsync(usuario);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(AlterarUsuarioAsync), ex.Message);
                throw;
            }
        }

        private async Task<Usuario> ConverteModelParaEntidade(UsuarioRegistroModel model)
        {
            return new Usuario
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                Role = model.Role,
            };
        }
        private async Task<UsuarioRegistroModel> ConverteEntidadeParaModel(Usuario model)
        {
            return new UsuarioRegistroModel
            {
                Nome = model.Nome,
                Email = model.Email,
                Senha = model.Senha,
                Role = model.Role,
            };
        }

        public async Task<UsuarioRegistroModel> BuscarUsuarioEmailAsync(string email)
        {
            try
            {
                var _usuario = await _usuarioRepository.BuscarUsuarioEmailAsync(email);
                if (_usuario == null)
                    return new UsuarioRegistroModel();

                return await ConverteEntidadeParaModel(_usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(BuscarUsuarioEmailAsync), ex.Message);
                throw;
            }
        }

        public async Task<UsuarioRegistroModel> BuscarUsuarioIdAsync(Guid id)
        {
            try
            {
                var _usuario = await _usuarioRepository.BuscarUsuarioIdAsync(id);
                if (_usuario == null)
                    return new UsuarioRegistroModel();

                return await ConverteEntidadeParaModel(_usuario);
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
                var _usuario = await _usuarioRepository.BuscarUsuarioIdAsync(id);
                if (_usuario == null)
                    return false;

                return await _usuarioRepository.DeletarUsuarioAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(DeletarUsuarioAsync), ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<UsuarioRegistroModel>> ListaUsuariosAsync()
        {
            try
            {
                var ListaUsuario = await _usuarioRepository.ListaUsuariosAsync();
                var listaUsuarioModel = new List<UsuarioRegistroModel>();
                if (ListaUsuario == null)
                    return listaUsuarioModel;


                foreach (var _usuario in ListaUsuario)
                {
                    listaUsuarioModel.Add(await ConverteEntidadeParaModel(_usuario));
                }
                return listaUsuarioModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar no método {MethodName}: {Message}", nameof(DeletarUsuarioAsync), ex.Message);
                throw;
            }
        }

        public async Task<bool> SalvarUsuarioAsync(UsuarioRegistroModel model)
        {
            try
            {
                var _usuario = await BuscarUsuarioEmailAsync(model.Email) ;
                if (_usuario != null)
                    return false;

                var usuario = await ConverteModelParaEntidade(model);
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
