using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioEasy.Data.Context;
using BioEasy.Data.Entities;

namespace BioEasy.Controllers
{
    public class AnaliseLaboratorialController : Controller
    {
        private readonly DatabaseContext _context;

        public AnaliseLaboratorialController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: AnaliseLaboratorial
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseContext = _context.AnalisesLaboratoriais.Include(a => a.Paciente).Where(h => h.PacienteId == id);
            ViewBag.Paciente = _context.Pacientes.Find(id).Nome;
            ViewBag.PacienteId = id;
            return View(await databaseContext.ToListAsync());
        }

        // GET: AnaliseLaboratorial/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiseLaboratorial = await _context.AnalisesLaboratoriais
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analiseLaboratorial == null)
            {
                return NotFound();
            }

            return View(analiseLaboratorial);
        }

        // GET: AnaliseLaboratorial/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Paciente = _context.Pacientes.Find(id).Nome;
            ViewBag.PacienteId = id;
            return View();
        }

        // POST: AnaliseLaboratorial/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Colesterol,LDL,HDL,Triglicerideos,AcucarNoSangue,UltimaRefeicao,DataLancamento,PacienteId,Id")] AnaliseLaboratorial analiseLaboratorial)
        {
            analiseLaboratorial.Id = 0;

            if (ModelState.IsValid)
            {
                _context.Add(analiseLaboratorial);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { Id = analiseLaboratorial.PacienteId });
            }

            ViewBag.Paciente = _context.Pacientes.Find(analiseLaboratorial.PacienteId).Nome;
            ViewBag.PacienteId = analiseLaboratorial.PacienteId;

            return View(analiseLaboratorial);
        }

        // GET: AnaliseLaboratorial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiseLaboratorial = await _context.AnalisesLaboratoriais.FindAsync(id);
            if (analiseLaboratorial == null)
            {
                return NotFound();
            }

            ViewBag.Paciente = _context.Pacientes.Find(analiseLaboratorial.PacienteId).Nome;
            ViewBag.PacienteId = analiseLaboratorial.PacienteId;

            return View(analiseLaboratorial);
        }

        // POST: AnaliseLaboratorial/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Colesterol,LDL,HDL,Triglicerideos,AcucarNoSangue,UltimaRefeicao,DataLancamento,PacienteId,Id")] AnaliseLaboratorial analiseLaboratorial)
        {
            if (id != analiseLaboratorial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analiseLaboratorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnaliseLaboratorialExists(analiseLaboratorial.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { Id = analiseLaboratorial.PacienteId });
            }

            ViewBag.Paciente = _context.Pacientes.Find(analiseLaboratorial.PacienteId).Nome;
            ViewBag.PacienteId = analiseLaboratorial.PacienteId;

            return View(analiseLaboratorial);
        }

        // GET: AnaliseLaboratorial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analiseLaboratorial = await _context.AnalisesLaboratoriais
                .Include(a => a.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analiseLaboratorial == null)
            {
                return NotFound();
            }

            return View(analiseLaboratorial);
        }

        // POST: AnaliseLaboratorial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analiseLaboratorial = await _context.AnalisesLaboratoriais.FindAsync(id);
            _context.AnalisesLaboratoriais.Remove(analiseLaboratorial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { Id = analiseLaboratorial.PacienteId });
        }

        private bool AnaliseLaboratorialExists(int id)
        {
            return _context.AnalisesLaboratoriais.Any(e => e.Id == id);
        }
    }
}
