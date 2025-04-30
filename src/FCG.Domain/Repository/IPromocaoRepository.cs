using FCG.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;

namespace FCG.Domain.Repository
{
    public interface IPromocaoRepository 
    {
        Task<bool> AlterarPromocaoAsync(Promocao promocao);
        Task<Promocao> BuscarPromocaoPorIdAsync(Guid id);

        Task<bool> DeletarPromocaoAsync(Guid id);
        Task<IList<Promocao>> ListarPromocaoAsync();
        Task<bool> SalvarPromocaoAsync(Promocao promocao);
    }
}