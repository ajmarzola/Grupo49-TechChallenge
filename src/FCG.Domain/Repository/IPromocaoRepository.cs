using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Repository
{
    public interface IPromocaoRepository
    {
        Task<Promocao> BuscarPromocaoIdAsync(Guid id);
        Task<IEnumerable<Promocao>> ListaPromocaoAsync();
        Task<bool> SalvarPromocaoAsync(Promocao promocao);
        Task<bool> AterarPromocaoAsync(Promocao promocao);
        Task<bool> DeletarPromocaoAsync(Guid id);
    }
}
