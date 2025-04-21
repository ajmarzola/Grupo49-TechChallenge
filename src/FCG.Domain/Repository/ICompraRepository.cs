using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Repository
{
    public interface ICompraRepository
    {
        Task<Compra> BuscarCompraIdAsync(Guid id);
        Task<IEnumerable<Compra>> ListaCompraAsync();
        Task<bool> SalvarCompraAsync(Compra compra);
        Task<bool> AlterarCompraAsync(Compra compra);
        Task<bool> DeletarCompraAsync(Guid id);
    }
}
