using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioEasy.Data.Context;
using BioEasy.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BioEasy.Controllers
{
    [Authorize]
    public class PacientesController : Controller
    {
        private readonly DatabaseContext _context;
        private static readonly string[] _SEXO = {"Masculino", "Feminino"};

        public PacientesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index(string searchString)
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            Usuario user = await _context.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();
            var pacientes = await _context.Pacientes.Where(p => p.EmpresaId == user.EmpresaId).ToListAsync();

            if (!string.IsNullOrEmpty(searchString)) 
            {
                pacientes = pacientes.Where(m => m.Nome.ToLower().Contains(searchString.ToLower())).ToList();
                ViewData["currentFilter"] = searchString;
            }

            return View(pacientes.OrderBy(m => m.Nome));
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            ViewBag.Sexo = _SEXO.Select(c => new SelectListItem()
                { Text = c, Value = c }).ToList();
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,DataNascimento,Altura,Sexo,Email,Endereco,Telefone,Cidade,Comentarios,Id")] Paciente paciente)
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            Usuario user = await _context.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();
            paciente.EmpresaId = user.EmpresaId;

            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { Id = paciente.Id });
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            ViewBag.Sexo = _SEXO.Select(c => new SelectListItem()
                { Text = c, Value = c }).ToList();
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,DataNascimento,Altura,Sexo,Email,Endereco,Telefone,Cidade,Comentarios,Id")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            Usuario user = await _context.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();
            paciente.EmpresaId = user.EmpresaId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { Id = paciente.Id });
            }
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
