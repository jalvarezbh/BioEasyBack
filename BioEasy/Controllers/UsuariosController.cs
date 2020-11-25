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

namespace BioEasy.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly DatabaseContext _context;

        public UsuariosController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(string searchString)
        {
            var usuarios = await _context.Usuarios.Include(a => a.Empresa).ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                usuarios = usuarios.Where(m => m.Nome.ToLower().Contains(searchString.ToLower())).ToList();
                ViewData["currentFilter"] = searchString;
            }

            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewBag.EmpresaId = _context.Empresas.Select(c => new SelectListItem()
                { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Nome,Senha,Email,Ativo,PrimeiroAcesso,Administrador,Id,EmpresaId,LoginDataAte")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.EmpresaId = _context.Empresas.Select(c => new SelectListItem()
                { Text = c.Nome, Value = c.Id.ToString() }).ToList();

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Senha,Email,Ativo,PrimeiroAcesso,Administrador,Id,EmpresaId,LoginDataAte")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<string> GetLoginExpira()
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var query = await _context.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();

            return (query.LoginDataAte.Subtract(DateTime.Now).TotalDays <= 30 ?  string.Concat("Acesso expira em ", query.LoginDataAte.ToString("dd/MM/yyyy")) : string.Empty);
        }
    }
}
