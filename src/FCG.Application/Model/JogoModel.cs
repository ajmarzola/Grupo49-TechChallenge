using FCG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.Model
{
    public class JogoModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public decimal Preco { get; set; }

        public string Categoria { get; set; } = string.Empty;

        //public ICollection<PromocaoModel> Promocoes { get; set; } = [];

        //public ICollection<CompraModel> Compras { get; set; } = [];
    }
}
