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

        public Task<bool> AlterarAsync(JogoModel model)
        {
            return _jogoRepository.AlterarAsync(Converter(model));
        }

        public Task<JogoModel> BuscarPorIdAsync(Guid id)
        {
            return _jogoRepository.BuscarPorIdAsync(id).ContinueWith(t => Converter(t.Result));
        }

        public Jogo Converter(JogoModel model)
        {
            ArgumentNullException.ThrowIfNull(model);

            return new Jogo
            {
                Id = model.Id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                Preco = model.Preco,
                Categoria = model.Categoria
            };
        }

        public JogoModel Converter(Jogo model)
        {
            ArgumentNullException.ThrowIfNull(model);

            return new JogoModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                Preco = model.Preco,
                Categoria = model.Categoria
            };
        }

        public IList<JogoModel> Converter(IList<Jogo> model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var jogoModels = new List<JogoModel>();

            foreach (var jogo in model)
            {
                jogoModels.Add(Converter(jogo));
            }

            return jogoModels;
        }

        public IList<Jogo> Converter(IList<JogoModel> model)
        {
            //ArgumentNullException.ThrowIfNull(model);
            throw new NotImplementedException();
        }

        public Task<bool> DeletarAsync(Guid id)
        {
            return _jogoRepository.DeletarAsync(id);
        }

        public async Task<IList<JogoModel>> ListarAsync()
        {
            var jogos = await _jogoRepository.ListarAsync();
            return Converter(jogos);
        }

        public Task<bool> SalvarAsync(JogoModel model)
        {
            return _jogoRepository.SalvarAsync(Converter(model));
        }
    }
}