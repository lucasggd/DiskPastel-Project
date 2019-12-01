using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiskPastel.Models;
using DiskPastel.Data;

namespace DiskPastel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var hvm = new HomeVM();
            var ids = _context.Destaques.Select(d => d.Idproduto).ToList();
            hvm.Destaques = _context.Produto.Where(p => ids.Contains(p.Idproduto)).ToList();
            return View(hvm);
        }

        public IActionResult Sobre()
        {
            return View();
        }
        public IActionResult Cardapio()
        {
            var tp = _context.TipoProduto.ToList(); // cria uma lista de tipos de produto
            var lcvm = new List<CardapioVM>();  // cria uma lista de cardapiovm
            foreach (var item in tp)  // para cada tipo de produto na lista de tipos de produto
            {
                var cvm = new CardapioVM();  // cria um novo cardapio
                cvm.TipoProduto = item.Nome;  // preenche o nome do tipo de produto no cardapio
                cvm.Cardapio = _context.Produto.Where(p => p.IdtipoProduto == item.IdtipoProduto).ToList();  // carrega a lista de produtos do cardapio
                // com os produtos do tipo do produto do foreach
                lcvm.Add(cvm);  // carrega o cardapio em uma lista de cardapios
            }
            return View(lcvm);  // retorna para a view uma lista de cardapios já separados por tipo
        }
        public IActionResult Contato()
        {
            return View();
        }
    }
}
