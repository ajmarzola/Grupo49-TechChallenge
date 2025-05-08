using FCG.Application.Model;
using FCG.Domain.Entities;
using FCG.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public class PromocaoService : IPromocaoService
    {
        private readonly IPromocaoRepository _promocaoRepository;

        public PromocaoService(IPromocaoRepository promocaoRepository)
        {
            _promocaoRepository = promocaoRepository;

        }
        public Task<bool> AlterarAsync(PromocaoModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PromocaoModel> BuscarPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        //AQUI
        public Promocao Converter(PromocaoModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new Promocao
            {
                Id = model.Id,
                Nome = model.Nome,
                DescontoPercentual = model.DescontoPercentual,
                DataInicio = model.DataInicio,
                DataFim = model.DataFim,
                JogoId = model.JogoId,
                Jogo = model.Jogo == null ? null : new Jogo
                {
                    Id = model.Jogo.Id,
                    Nome = model.Jogo.Nome,
                    Descricao = model.Jogo.Descricao,
                    Preco = model.Jogo.Preco,
                    Categoria = model.Jogo.Categoria                   
                }
            };
        }

        public PromocaoModel Converter(Promocao model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new PromocaoModel
            {
                Id = model.Id,
                Nome = model.Nome,
                DescontoPercentual = model.DescontoPercentual,
                DataInicio = model.DataInicio,
                DataFim = model.DataFim,
                JogoId = model.JogoId,
                Jogo = model.Jogo == null ? null : new JogoModel
                {
                    Id = model.Jogo.Id,
                    Nome = model.Jogo.Nome,
                    Descricao = model.Jogo.Descricao,
                    Preco = model.Jogo.Preco,
                    Categoria = model.Jogo.Categoria
                    // Add Promocoes or Compras only if needed
                }
            };
        }

        public IList<PromocaoModel> Converter(IList<Promocao> model)
        {
            return model.Select(Converter).ToList();
        }

        public IList<Promocao> Converter(IList<PromocaoModel> model)
        {
            return model.Select(Converter).ToList();
        }

        public Task<bool> DeletarAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<PromocaoModel>> ListarAsync()
        {
            var promocoes = await _promocaoRepository.ListarAsync();
            return Converter(promocoes);
        }

        public Task<bool> SalvarAsync(PromocaoModel model)
        {
            return _promocaoRepository.SalvarAsync(Converter(model));
        }
    }
}
