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
        public async Task<bool> AlterarAsync(PromocaoModel model)
        {
            try
            {
                var existente = await _promocaoRepository.BuscarPorIdAsync(model.Id);

                if (existente == null)
                    throw new KeyNotFoundException($"Promoção com ID {model.Id} não encontrada para atualização.");

                var entity = Converter(model);
                return await _promocaoRepository.AlterarAsync(entity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao alterar promoção: {ex.Message}", ex);
            }
        }

        public async Task<PromocaoModel> BuscarPorIdAsync(Guid id)
        {
            try
            {
                var promocao = await _promocaoRepository.BuscarPorIdAsync(id);

                if (promocao == null)
                {
                    throw new KeyNotFoundException($"Promoção com ID {id} não encontrada.");
                }

                return Converter(promocao);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao buscar promoção por ID: {ex.Message}", ex);
            }
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
                JogoId = model.JogoId
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
                JogoId = model.JogoId
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

        public async Task<bool> DeletarAsync(Guid id)
        {
            try
            {
                var existente = await _promocaoRepository.BuscarPorIdAsync(id);

                if (existente == null)
                    throw new KeyNotFoundException($"Promoção com ID {id} não encontrada para exclusão.");

                return await _promocaoRepository.DeletarAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao deletar promoção: {ex.Message}", ex);
            }
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
