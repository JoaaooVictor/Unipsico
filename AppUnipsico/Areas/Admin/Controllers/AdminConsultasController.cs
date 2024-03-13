using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppUnipsico.Data.Context;
using AppUnipsico.Models;

namespace AppUnipsico.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminConsultasController : Controller
    {
        private readonly AppUnipsicoDb _context;

        public AdminConsultasController(AppUnipsicoDb context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appUnipsicoDb = _context.Consultas
                .Include(c => c.Usuario)
                .Include(c => c.DataConsulta);

            return View(await appUnipsicoDb.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Usuario)
                .Include(c => c.DataConsulta)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        public IActionResult Create()
        {
            ViewData["ConsultaId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsultaId,DataConsulta,StatusConsulta,UsuarioId")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsultaId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", consulta.ConsultaId);
            return View(consulta);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }
            ViewData["ConsultaId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", consulta.ConsultaId);
            return View(consulta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConsultaId,DataConsulta,StatusConsulta,UsuarioId")] Consulta consulta)
        {
            if (id != consulta.ConsultaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consulta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultaExists(consulta.ConsultaId))
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
            ViewData["ConsultaId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", consulta.ConsultaId);
            return View(consulta);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consulta = await _context.Consultas
                .Include(c => c.Usuario)
                .Include(c => c.DataConsulta)
                .FirstOrDefaultAsync(m => m.ConsultaId == id);
            if (consulta == null)
            {
                return NotFound();
            }

            return View(consulta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(string id)
        {
            return _context.Consultas.Any(e => e.ConsultaId == id);
        }
    }
}
