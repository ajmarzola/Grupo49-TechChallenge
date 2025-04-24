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
            return _jogoRepository.AlterarJogoAsync(Converter(model));
        }

        public Task<JogoModel> BuscarPorIdAsync(Guid id)
        {
            return _jogoRepository.BuscarJogoIdAsync(id).ContinueWith(t => Converter(t.Result));
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
            ArgumentNullException.ThrowIfNull(model);

            var jogos = new List<Jogo>();

            foreach (var jogoModel in model)
            {
                jogos.Add(Converter(jogoModel));
            }

            return jogos;
        }

        public Task<bool> DeletarAsync(Guid id)
        {
            return _jogoRepository.DeletarJogoAsync(id);
        }

        public Task<IList<JogoModel>> ListarAsync()
        {
            return _jogoRepository.ListaJogoAsync().ContinueWith(t => Converter(t.Result));
        }

        private IList<JogoModel> Converter(object result)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SalvarAsync(JogoModel model)
        {
            return _jogoRepository.SalvarJogoAsync(Converter(model));
        }
    }
}