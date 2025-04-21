using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Services
{
    internal class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService (IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public Task<bool> AlterarJogo(Jogo model)
        {
            throw new NotImplementedException();
        }

        public Task<Jogo> BucarJogo(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarJogo(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Jogo>> ListaJogo()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SavarJogo(Jogo model)
        {
            throw new NotImplementedException();
        }
    }
}
