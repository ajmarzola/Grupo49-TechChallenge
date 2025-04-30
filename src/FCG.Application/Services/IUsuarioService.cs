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
        Task<UsuarioRegistroModel> BuscarUsuarioIdAsync(Guid id);
        Task<UsuarioRegistroModel> BuscarUsuarioEmailAsync(string email);
        Task<IEnumerable<UsuarioRegistroModel>> ListaUsuariosAsync();
        Task<bool> SalvarUsuarioAsync(UsuarioRegistroModel usuario);
        Task<bool> AlterarUsuarioAsync(UsuarioRegistroModel usuario);
        Task<bool> DeletarUsuarioAsync(Guid id);
    }
}
