using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BioEasy.Data.Context;
using BioEasy.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Mime;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BioEasy.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly DatabaseContext _context;
        private static readonly string[] _BALANCA = { "RD545", "BC601" };
        private readonly IHostEnvironment _host;

        public EmpresasController(DatabaseContext context, IHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index(string searchString)
        {
            var empresas = await _context.Empresas.ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                empresas = empresas.Where(m => m.Nome.ToLower().Contains(searchString.ToLower())).ToList();
                ViewData["currentFilter"] = searchString;
            }

            return View(empresas);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewBag.Balanca = _BALANCA.Select(c => new SelectListItem() { Text = c, Value = c }).ToList();
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Nome,CRN_CRM,CPF_CNPJ,Email,Endereco,Instagram,Telefone,Logo,Id,Balanca")] Empresa empresa, IFormFile Logo)
        {
            if (ModelState.IsValid)
            {
                if (Logo != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        Logo.CopyTo(ms);
                        empresa.Logo = ms.ToArray();
                    }
                }
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        string imgPath = string.Concat(_host.ContentRootPath, "\\wwwroot\\img\\logo_small.PNG");
                        empresa.Logo = System.IO.File.ReadAllBytes(imgPath);
                    }
                }

                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }

            ViewBag.Balanca = _BALANCA.Select(c => new SelectListItem() { Text = c, Value = c }).ToList();
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,CRN_CRM,CPF_CNPJ,Email,Endereco,Instagram,Telefone,Logo,Id,Balanca")] Empresa empresa, IFormFile Logo)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Logo != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        Logo.CopyTo(ms);
                        empresa.Logo = ms.ToArray();
                    }
                }
                else
                {
                    using (var ms = new MemoryStream())
                    {
                        string imgPath = string.Concat(_host.ContentRootPath, "\\wwwroot\\img\\logo_small.PNG");
                        empresa.Logo = System.IO.File.ReadAllBytes(imgPath);
                    }
                }

                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<string> GetLogo() 
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var query = await _context.Usuarios.Include(h => h.Empresa).Where(u => u.Email == email).FirstOrDefaultAsync();
            return @Convert.ToBase64String(query.Empresa.Logo);
        }

        // GET: Empresas/Edit/5
        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> EditarEmpresa()
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            Usuario user = await _context.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();
            var empresa = await _context.Empresas.FindAsync(user.EmpresaId);

            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Usuario")]
        public async Task<IActionResult> EditarEmpresa(int id, string Endereco, string Instagram, string Telefone)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            empresa.Endereco = Endereco;
            empresa.Instagram = Instagram;
            empresa.Telefone = Telefone;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(empresa);
        }
    }
}
