using Azure;
using FCG.Application.Model;
using FCG.Application.Services;
using FCG.Domain.Entities;
using FCG.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FCG.Domain.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<bool> AlterarAsync(Jogo jogo)
        {
            try
            {
                return await _jogoRepository.AlterarAsync(jogo);
               
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<Jogo> BuscarPorIdAsync(Guid id)
        {
            try
            {
                var jogo = _jogoRepository.BuscarPorIdAsync(id);

                if (jogo == null)
                {
                    throw new KeyNotFoundException($"Jogo {jogo} não encontroado.");
                }

                return jogo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletarAsync(Guid id)
        {
            try
            {
                return await _jogoRepository.DeletarAsync(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IList<Jogo>> ListarAsync()
        {
            IList<Jogo> ListJogos = [];

            try
            {
                ListJogos =  await _jogoRepository.ListarAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListJogos;
        }

        public async Task<bool> SalvarAsync(Jogo model)
        {
            try
            {
                return await _jogoRepository.SalvarAsync(model);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}