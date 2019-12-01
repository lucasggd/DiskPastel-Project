using System;
using System.Collections.Generic;

namespace DiskPastel.Models
{
    public partial class Produto
    {
        public Produto()
        {
            Destaques = new HashSet<Destaques>();
        }

        public int Idproduto { get; set; }
        public string Nome { get; set; }
        public string Custo { get; set; }
        public string Venda { get; set; }
        public int IdtipoProduto { get; set; }

        public TipoProduto IdtipoProdutoNavigation { get; set; }
        public ICollection<Destaques> Destaques { get; set; }
    }
}
