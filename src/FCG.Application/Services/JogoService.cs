using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCG.Domain.Services
{
    internal class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public Task<bool> AlterarAsync(Jogo model)
        {
            throw new NotImplementedException();
        }

        public Task<Jogo> BuscarPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Jogo>> ListarAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SalvarAsync(Jogo model)
        {
            throw new NotImplementedException();
        }
    }
}