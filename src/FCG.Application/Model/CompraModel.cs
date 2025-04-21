using FCG.Application.DTOs;
using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Model
{
    public class CompraModel
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        public UsuarioRegistroModel? Usuario { get; set; }

        public Guid JogoId { get; set; }

        public Jogo? Jogo { get; set; }

        public DateTime DataCompra { get; set; }

        public decimal ValorPago { get; set; }
    }
}
