using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Entities
{
    public class Jogo
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public ICollection<Promocao> Promocoes { get; set; } = new List<Promocao>();
        public ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
}
