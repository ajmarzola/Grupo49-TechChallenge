using FCG.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace FCG.Domain.Repository
{
    public interface IJogoRepository 
    {
        Task<Jogo> BuscarJogoIdAsync(Guid id);
        Task<IEnumerable<Jogo>> ListaJogoAsync();
        Task<bool> SalvarJogoAsync(Jogo usuario);
        Task<bool> AlterarJogoAsync(Jogo usuario);
        Task<bool> DeletarJogoAsync(Guid id);

    }
}