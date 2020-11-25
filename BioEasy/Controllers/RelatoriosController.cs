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
using AutoMapper;
using BioEasy.ViewModels;
using PuppeteerSharp;
using IronPdf;
using CoreHtmlToImage;

namespace BioEasy.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly DatabaseContext _context;
        IMapper _mapper;

        public RelatoriosController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Paciente = _context.Pacientes.Find(id).Nome;
            ViewBag.PacienteId = id;

            return View();
        }

        public async Task<IActionResult> Prevencao(int? id, string pdf, string email)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteData = await _context.Pacientes.FindAsync(id);
            var historico = _context.HistoricoPacientes.Where(m => m.PacienteId == id).OrderBy(m => m.DataRegistroBalanca).FirstOrDefault();
            var analise = _context.AnalisesLaboratoriais.Where(m => m.PacienteId == id).OrderBy(m => m.DataLancamento).FirstOrDefault();
            var paciente = _mapper.Map<Prevencao>(pacienteData);
            paciente.HistoricoPaciente = historico;
            paciente.AnaliseLaboratorial = analise;

            if (paciente.HistoricoPaciente == null)
            {
                paciente.HistoricoPaciente = new HistoricoPaciente();
            }

            if (paciente.AnaliseLaboratorial == null)
            {
                paciente.AnaliseLaboratorial = new AnaliseLaboratorial();
            }

            ViewBag.PacienteId = id;

            if (paciente == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(email))
            {
                var query = await _context.Usuarios.Include(m => m.Empresa).Where(u => u.Email == email).FirstOrDefaultAsync();
                ViewBag.NomeEmpresa = query.Empresa.Nome;
                ViewBag.CRN = query.Empresa.CRN_CRM;
                ViewBag.Logo = Convert.ToBase64String(query.Empresa.Logo);
                ViewBag.PDF = pdf;
                ViewBag.Balanca = query.Empresa.Balanca;
            }
            else {
                var _email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
                var query = await _context.Usuarios.Include(m => m.Empresa).Where(u => u.Email == _email).FirstOrDefaultAsync();
                ViewBag.Balanca = query.Empresa.Balanca;
            }

            return View(paciente);
        }

        public async Task<IActionResult> Progresso(int? id, string pdf, string email)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteData = await _context.Pacientes.FindAsync(id);
            var historico = _context.HistoricoPacientes.Where(m => m.PacienteId == id).OrderBy(m => m.DataRegistroBalanca).Take(5);
            var analise =  _context.AnalisesLaboratoriais.Where(m => m.PacienteId == id).OrderBy(m => m.DataLancamento).Take(5);
            var paciente = _mapper.Map<Progresso>(pacienteData);
            paciente.HistoricoPaciente = new List<HistoricoPaciente>();
            paciente.AnaliseLaboratorial = new List<AnaliseLaboratorial>();
            paciente.HistoricoPaciente.AddRange(historico);
            paciente.AnaliseLaboratorial.AddRange(analise);

            if (paciente.HistoricoPaciente.Count() == 0)
            {
                paciente.HistoricoPaciente.Add(new HistoricoPaciente());
            }

            if (paciente.AnaliseLaboratorial.Count() == 0)
            {
                paciente.AnaliseLaboratorial.Add(new AnaliseLaboratorial());
            }

            ViewBag.PacienteId = id;

            if (paciente == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(email))
            {
                var query = await _context.Usuarios.Include(m => m.Empresa).Where(u => u.Email == email).FirstOrDefaultAsync();
                ViewBag.NomeEmpresa = query.Empresa.Nome;
                ViewBag.CRN = query.Empresa.CRN_CRM;
                ViewBag.Logo = Convert.ToBase64String(query.Empresa.Logo);
                ViewBag.PDF = pdf;
                ViewBag.Balanca = query.Empresa.Balanca;
            }
            else
            {
                var _email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
                var query = await _context.Usuarios.Include(m => m.Empresa).Where(u => u.Email == _email).FirstOrDefaultAsync();
                ViewBag.Balanca = query.Empresa.Balanca;
            }

            return View(paciente);
        }

        public async Task<IActionResult> AnaliseAtual(int? id, string pdf, string email)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteData = await _context.Pacientes.FindAsync(id);
            var historico = _context.HistoricoPacientes.Where(m => m.PacienteId == id).OrderBy(m => m.DataRegistroBalanca).Take(5);
            var analise = _context.AnalisesLaboratoriais.Where(m => m.PacienteId == id).OrderBy(m => m.DataLancamento).Take(5);
            var paciente = _mapper.Map<AnaliseAtual>(pacienteData);
            paciente.HistoricoPaciente = new List<HistoricoPaciente>();
            paciente.AnaliseLaboratorial = new List<AnaliseLaboratorial>();
            paciente.HistoricoPaciente.AddRange(historico);
            paciente.AnaliseLaboratorial.AddRange(analise);

            if (paciente.HistoricoPaciente.Count() == 0) {
                paciente.HistoricoPaciente.Add(new HistoricoPaciente());
            }

            if (paciente.AnaliseLaboratorial.Count() == 0)
            {
                paciente.AnaliseLaboratorial.Add(new AnaliseLaboratorial());
            }

            ViewBag.PacienteId = id;

            if (paciente == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(email)) 
            { 
                var query = await _context.Usuarios.Include(m => m.Empresa).Where(u => u.Email == email).FirstOrDefaultAsync();
                ViewBag.NomeEmpresa = query.Empresa.Nome;
                ViewBag.CRN = query.Empresa.CRN_CRM;
                ViewBag.Logo = Convert.ToBase64String(query.Empresa.Logo);
                ViewBag.PDF = pdf;
                ViewBag.Balanca = query.Empresa.Balanca;
            }
            else
            {
                var _email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
                var query = await _context.Usuarios.Include(m => m.Empresa).Where(u => u.Email == _email).FirstOrDefaultAsync();
                ViewBag.Balanca = query.Empresa.Balanca;
            }

            return View(paciente);
        }

        public async Task<IActionResult> GeneratePDF1(int? pacienteId, string relatorio)
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true
            });
            var request = HttpContext.Request;
            var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            var page = await browser.NewPageAsync();
            await page.GoToAsync($"{absoluteUri}/Relatorios/{relatorio}?id={pacienteId}&pdf=PDF&email={email}");
            var file = await page.PdfStreamAsync();

            using (BinaryReader br = new BinaryReader(file))
            {
                return File(br.ReadBytes((int)file.Length), "application/pdf");
            }
        }

        public async Task<IActionResult> GeneratePDF2(int? pacienteId, string relatorio)
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var request = HttpContext.Request;
            var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

            // Create a PDF from any existing web page
            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderUrlAsPdf($"{absoluteUri}/Relatorios/{relatorio}?id={pacienteId}&pdf=PDF&email={email}");
            // Create a PDF from an existing HTML
            Renderer.PrintOptions.MarginTop = 50;  //millimetres
            Renderer.PrintOptions.MarginBottom = 50;
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            Renderer.PrintOptions.Header = new SimpleHeaderFooter()
            {
                CenterText = "{pdf-title}",
                DrawDividerLine = true,
                FontSize = 16
            };
            Renderer.PrintOptions.Footer = new SimpleHeaderFooter()
            {
                LeftText = "{date} {time}",
                RightText = "Page {page} of {total-pages}",
                DrawDividerLine = true,
                FontSize = 14
            };
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 2000; //milliseconds
            return File(PDF.BinaryData, "application/pdf"); 
        }

        public async Task<IActionResult> GeneratePDF(int? pacienteId, string relatorio)
        {
            var email = User.Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var request = HttpContext.Request;
            var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

            var converter = new HtmlConverter();
            var bytes = converter.FromUrl($"{absoluteUri}/Relatorios/{relatorio}?id={pacienteId}&pdf=PDF&email={email}");
            return File(bytes, "image/jpg");
        }
    }
}