using System;

namespace FCG.Domain.Entities
{
    public class Promocao
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public decimal DescontoPercentual { get; set; } // ex: 10 para 10%

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public Guid JogoId { get; set; }

        public Jogo? Jogo { get; set; }
    }
}