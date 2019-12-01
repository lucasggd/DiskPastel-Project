using System;
using System.Collections.Generic;

namespace DiskPastel.Models
{
    public partial class TipoProduto
    {
        public TipoProduto()
        {
            Produto = new HashSet<Produto>();
        }

        public int IdtipoProduto { get; set; }
        public string Nome { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}
