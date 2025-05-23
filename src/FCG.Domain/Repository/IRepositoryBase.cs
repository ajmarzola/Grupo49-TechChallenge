using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCG.Domain.Repository
{
    public interface IRepositoryBase<T>
    {
        Task<IList<T>> ListarAsync();

        Task<T> BuscarPorIdAsync(Guid id);

        Task<bool> SalvarAsync(T model);

        Task<bool> AlterarAsync(T model);

        Task<bool> DeletarAsync(Guid id);
    }
}