using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Repository
{
    public interface IJogoRepository
    {
        Task<List<Jogo>> ListaJogo();
        Task<Jogo> BucarJogo(Guid id);
        Task<bool> SavarJogo(Jogo model);
        Task<bool> AlterarJogo(Jogo model);
        Task<bool> DeletarJogo(Guid id);
    }
}
