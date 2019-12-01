using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiskPastel.Models
{
    public class CardapioVM
    {
        public string TipoProduto { get; set; }
        public List<Produto> Cardapio { get; set; }
    }
}
