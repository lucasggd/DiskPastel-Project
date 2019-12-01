using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DiskPastel.Data;
using DiskPastel.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace DiskPastel.Controllers
{
    public class ProdutoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IHostingEnvironment he;

        private readonly IConfiguration _config;

        public ProdutoesController(ApplicationDbContext context, IHostingEnvironment e, IConfiguration config)
        {
            _context = context;
            he = e;
            _config = config;
        }

        // GET: Produtoes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produto.Include(p => p.IdtipoProdutoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Produtoes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.IdtipoProdutoNavigation)
                .FirstOrDefaultAsync(m => m.Idproduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            var filename = "/images/cardapio/" + id + ".jpg";
            ViewData["filelocation"] = filename;

            return View(produto);
        }

        // GET: Produtoes/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["IdtipoProduto"] = new SelectList(_context.TipoProduto, "IdtipoProduto", "Nome");
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idproduto,Nome,Custo,Venda,IdtipoProduto")] Produto produto, IFormFile pic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();

                string produtos = produto.Idproduto + ".jpg";

                var caminho = _config.GetValue<string>("Upload:Imagens");

                var arquivo = Path.Combine(he.WebRootPath, caminho, produtos);

                FileStream fileStream = new FileStream(arquivo, FileMode.Create);
                pic.CopyTo(fileStream);
                fileStream.Close();

                return RedirectToAction(nameof(Index));

            }
            ViewData["IdtipoProduto"] = new SelectList(_context.TipoProduto, "IdtipoProduto", "Nome", produto.IdtipoProduto);
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            var filename = "/images/cardapio/" + id + ".jpg";
            ViewData["filelocation"] = filename;

            ViewData["IdtipoProduto"] = new SelectList(_context.TipoProduto, "IdtipoProduto", "Nome", produto.IdtipoProduto);
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idproduto,Nome,Custo,Venda,IdtipoProduto")] Produto produto, IFormFile pic)
        {
            if (id != produto.Idproduto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    string produtos = produto.Idproduto + ".jpg";
                    var caminho = _config.GetValue<string>("Upload:Imagens");
                    var arquivo = Path.Combine(he.WebRootPath, caminho, produtos);
                    System.IO.File.Delete(arquivo);

                    FileStream fileStream = new FileStream(arquivo, FileMode.Create);
                    pic.CopyTo(fileStream);
                    fileStream.Close();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Idproduto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdtipoProduto"] = new SelectList(_context.TipoProduto, "IdtipoProduto", "Nome", produto.IdtipoProduto);
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.IdtipoProdutoNavigation)
                .FirstOrDefaultAsync(m => m.Idproduto == id);
            if (produto == null)
            {
                return NotFound();
            }

            var filename = "/images/cardapio/" + id + ".jpg";
            ViewData["filelocation"] = filename;

            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            string produtos = produto.Idproduto + ".jpg";
            var caminho = _config.GetValue<string>("Upload:Imagens");
            var arquivo = Path.Combine(he.WebRootPath, caminho, produtos);
            System.IO.File.Delete(arquivo);

            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Idproduto == id);
        }

    }
}
