using System;
using System.Collections.Generic;

namespace DiskPastel.Models
{
    public partial class Destaques
    {
        public int Iddestaque { get; set; }
        public DateTime Data { get; set; }
        public int Idproduto { get; set; }

        public Produto IdprodutoNavigation { get; set; }
    }
}
