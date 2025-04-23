using FCG.Domain.Entities;
using FCG.Application.Model;
using System.Collections.Generic;

namespace FCG.Application.Services
{
    public interface IJogoService : IServiceBase<JogoModel>
    {
        public Jogo Converter(JogoModel model);

        public JogoModel Converter(Jogo model);

        public IList<JogoModel> Converter(IList<Jogo> model);

        public IList<Jogo> Converter(IList<JogoModel> model);
    }
}