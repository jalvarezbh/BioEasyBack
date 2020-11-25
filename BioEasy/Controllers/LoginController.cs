using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BioEasy.Data.Context;
using BioEasy.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BioEasy.Controllers
{    
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]      
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario user = await _context.Usuarios.Where(u => u.Email == email && u.Senha == password && u.Ativo).FirstOrDefaultAsync();

            if (user == null) 
            {
                ViewBag.LoginInvalido = "Usuário ou senha incorretos.";
                return View("SignIn");
            }

            if (user.LoginDataAte <= DateTime.Now)
            {
                ViewBag.LoginInvalido = "Login expirou. Entre em contato com o administrador do sistema.";
                return View("SignIn");
            }
                
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Administrador ? "Administrador" : "Usuario"),
            };

            var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
            await HttpContext.SignInAsync(userPrincipal);

            return user.PrimeiroAcesso ? RedirectToAction("Contrato", "Login") : RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return View("SignIn");
        }

        public IActionResult Contrato()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Aceito()
        {
            try
            {
                var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
                Usuario user = await _context.Usuarios.Where(u => u.Email == email).FirstOrDefaultAsync();
                user.PrimeiroAcesso = false;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}