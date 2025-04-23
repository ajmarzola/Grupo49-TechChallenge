using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Repository
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario> BuscarUsuarioIdAsync(Guid id);
        Task<Usuario> BuscarUsuarioEmailAsync(string email);
        Task<IEnumerable<Usuario>> ListaUsuariosAsync();
        Task<bool> SalvarUsuarioAsync(Usuario usuario);
        Task<bool> AlterarAsync(Usuario usuario);
        Task<bool> DeletarUsuarioAsync(Guid id);
    }
}
