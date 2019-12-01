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

namespace DiskPastel.Controllers
{
    public class TipoProdutoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoProdutoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoProdutoes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoProduto.ToListAsync());
        }

        // GET: TipoProdutoes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto
                .FirstOrDefaultAsync(m => m.IdtipoProduto == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProdutoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdtipoProduto,Nome")] TipoProduto tipoProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto.FindAsync(id);
            if (tipoProduto == null)
            {
                return NotFound();
            }
            return View(tipoProduto);
        }

        // POST: TipoProdutoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdtipoProduto,Nome")] TipoProduto tipoProduto)
        {
            if (id != tipoProduto.IdtipoProduto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProdutoExists(tipoProduto.IdtipoProduto))
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
            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto
                .FirstOrDefaultAsync(m => m.IdtipoProduto == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // POST: TipoProdutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoProduto = await _context.TipoProduto.FindAsync(id);
            _context.TipoProduto.Remove(tipoProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProdutoExists(int id)
        {
            return _context.TipoProduto.Any(e => e.IdtipoProduto == id);
        }
    }
}
