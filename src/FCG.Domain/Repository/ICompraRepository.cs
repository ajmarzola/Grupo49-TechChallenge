using FCG.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FCG.Domain.Repository
{
    public interface ICompraRepository 
    {
        Task<Compra> BuscarCompraPorIdAsync(Guid id);
        Task<IList<Compra>> ListarCompraAsync();

        Task<bool> SalvarCompraAsync(Compra compra);

        Task<bool> AlterarCompraAsync(Compra compra);

        Task<bool> DeletarCompraAsync(Guid id);
    }
}