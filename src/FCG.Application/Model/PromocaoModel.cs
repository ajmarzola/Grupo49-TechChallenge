using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Model
{
    public class PromocaoModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public decimal DescontoPercentual { get; set; } // ex: 10 para 10%

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public Guid JogoId { get; set; }
    }
}
