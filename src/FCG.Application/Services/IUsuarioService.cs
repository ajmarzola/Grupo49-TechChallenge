using FCG.Application.DTOs;
using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> BuscarUsuarioIdAsync(Guid id);
        Task<Usuario> BuscarUsuarioEmailAsync(string email);
        Task<IEnumerable<Usuario>> ListaUsuariosAsync();
        Task<bool> SalvarUsuarioAsync(UsuarioRegistroModel usuario);
        Task<bool> AlterarAsync(UsuarioRegistroModel usuario);
        Task<bool> DeletarUsuarioAsync(Guid id);
    }
}