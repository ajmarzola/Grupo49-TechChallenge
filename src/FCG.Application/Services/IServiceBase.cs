using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public interface IServiceBase<T>
    {
        Task<IList<T>> ListarAsync();

        Task<T> BuscarPorIdAsync(Guid id);

        Task<bool> SalvarAsync(T model);

        Task<bool> AlterarAsync(T model);

        Task<bool> DeletarAsync(Guid id);
    }
}