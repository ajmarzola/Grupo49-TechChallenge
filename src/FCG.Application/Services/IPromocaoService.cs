using FCG.Application.Model;
using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Services
{
    public interface IPromocaoService : IServiceBase<PromocaoModel>
    {
        public Promocao Converter(PromocaoModel model);

        public PromocaoModel Converter(Promocao model);

        public IList<PromocaoModel> Converter(IList<Promocao> model);

        public IList<Promocao> Converter(IList<PromocaoModel> model);
    }
}
