using System;

namespace FCG.Domain.Entities
{
    public class Compra
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        public Guid JogoId { get; set; }

        public Jogo? Jogo { get; set; }

        public DateTime DataCompra { get; set; }

        public decimal ValorPago { get; set; }
    }
}