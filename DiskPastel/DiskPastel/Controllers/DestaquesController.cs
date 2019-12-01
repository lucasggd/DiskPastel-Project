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
    public class DestaquesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DestaquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Destaques
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Destaques.Include(d => d.IdprodutoNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Destaques/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destaques = await _context.Destaques
                .Include(d => d.IdprodutoNavigation)
                .FirstOrDefaultAsync(m => m.Iddestaque == id);
            if (destaques == null)
            {
                return NotFound();
            }

            return View(destaques);
        }

        // GET: Destaques/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["Idproduto"] = new SelectList(_context.Produto, "Idproduto", "Nome");
            return View();
        }

        // POST: Destaques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Iddestaque,Data,Idproduto")] Destaques destaques)
        {
            if (ModelState.IsValid)
            {
                _context.Add(destaques);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idproduto"] = new SelectList(_context.Produto, "Idproduto", "Nome", destaques.Idproduto);
            return View(destaques);
        }

        // GET: Destaques/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destaques = await _context.Destaques.FindAsync(id);
            if (destaques == null)
            {
                return NotFound();
            }
            ViewData["Idproduto"] = new SelectList(_context.Produto, "Idproduto", "Nome", destaques.Idproduto);
            return View(destaques);
        }

        // POST: Destaques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Iddestaque,Data,Idproduto")] Destaques destaques)
        {
            if (id != destaques.Iddestaque)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(destaques);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DestaquesExists(destaques.Iddestaque))
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
            ViewData["Idproduto"] = new SelectList(_context.Produto, "Idproduto", "Nome", destaques.Idproduto);
            return View(destaques);
        }

        // GET: Destaques/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destaques = await _context.Destaques
                .Include(d => d.IdprodutoNavigation)
                .FirstOrDefaultAsync(m => m.Iddestaque == id);
            if (destaques == null)
            {
                return NotFound();
            }

            return View(destaques);
        }

        // POST: Destaques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var destaques = await _context.Destaques.FindAsync(id);
            _context.Destaques.Remove(destaques);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DestaquesExists(int id)
        {
            return _context.Destaques.Any(e => e.Iddestaque == id);
        }
    }
}
