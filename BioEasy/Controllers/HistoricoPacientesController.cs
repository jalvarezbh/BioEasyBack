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
using CsvHelper;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BioEasy.Controllers
{
    public class HistoricoPacientesController : Controller
    {
        private readonly DatabaseContext _context;

        public HistoricoPacientesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: HistoricoPacientes
        public async Task<IActionResult> Index(int? id, string mensagem)
        {
            if (id == null)
            {
                return NotFound();
            }

            var databaseContext = _context.HistoricoPacientes.Include(h => h.Paciente).Where(h => h.PacienteId == id);
            ViewBag.Paciente = _context.Pacientes.Find(id).Nome;
            ViewBag.PacienteId = id;
            ViewBag.Mensagem = mensagem;
            return View(await databaseContext.ToListAsync());
        }

        // GET: HistoricoPacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoPaciente = await _context.HistoricoPacientes
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historicoPaciente == null)
            {
                return NotFound();
            }

            return View(historicoPaciente);
        }

        // GET: HistoricoPacientes/Create
        public IActionResult Create(int? id)
        {
            ViewBag.Paciente = _context.Pacientes.Find(id).Nome;
            ViewBag.PacienteId = id;
            return View();
        }

        // POST: HistoricoPacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Peso,NivelGordura,AguaCorporal,GorduraVisceral,MassaMuscular,MassaOssea,IdadeMetabolica,MassaAdiposa,MassaNaoAdiposa,IngestaoCalorica,TaxaMetabolicaBasal,MassaCorporal,QualidadeMuscularTotal,PernaDireitaNivelGordura,PernaDireitaMassaAdiposa,PernaDireitaMassaNaoAdiposa,PernaDireitaMassaMuscular,PernaDireitaQualidadeMuscular,PernaEsquerdaNivelGordura,PernaEsquerdaMassaAdiposa,PernaEsquerdaMassaNaoAdiposa,PernaEsquerdaMassaMuscular,PernaEsquerdaQualidadeMuscular,BracoDireitoNivelGordura,BracoDireitoMassaAdiposa,BracoDireitoMassaNaoAdiposa,BracoDireitoMassaMuscular,BracoDireitoQualidadeMuscular,BracoEsquerdoNivelGordura,BracoEsquerdoMassaAdiposa,BracoEsquerdoMassaNaoAdiposa,BracoEsquerdoMassaMuscular,BracoEsquerdoQualidadeMuscular,TroncoNivelGordura,TroncoMassaAdiposa,TroncoMassaNaoAdiposa,TroncoMassaMuscular,DataRegistroBalanca,DataAtualizacao,PacienteId,Id")] HistoricoPaciente historicoPaciente)
        {
            historicoPaciente.Id = 0;

            if (ModelState.IsValid)
            {
                historicoPaciente.DataAtualizacao = DateTime.Now;
                _context.Add(historicoPaciente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { Id = historicoPaciente.PacienteId });
            }

            ViewBag.Paciente = _context.Pacientes.Find(historicoPaciente.PacienteId).Nome;
            ViewBag.PacienteId = historicoPaciente.PacienteId;
            return View(historicoPaciente);
        }

        // GET: HistoricoPacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoPaciente = await _context.HistoricoPacientes.FindAsync(id);
            if (historicoPaciente == null)
            {
                return NotFound();
            }

            ViewBag.Paciente = _context.Pacientes.Find(historicoPaciente.PacienteId).Nome;
            ViewBag.PacienteId = historicoPaciente.PacienteId;

            return View(historicoPaciente);
        }

        // POST: HistoricoPacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Peso,NivelGordura,AguaCorporal,GorduraVisceral,MassaMuscular,MassaOssea,IdadeMetabolica,MassaAdiposa,MassaNaoAdiposa,IngestaoCalorica,TaxaMetabolicaBasal,MassaCorporal,QualidadeMuscularTotal,PernaDireitaNivelGordura,PernaDireitaMassaAdiposa,PernaDireitaMassaNaoAdiposa,PernaDireitaMassaMuscular,PernaDireitaQualidadeMuscular,PernaEsquerdaNivelGordura,PernaEsquerdaMassaAdiposa,PernaEsquerdaMassaNaoAdiposa,PernaEsquerdaMassaMuscular,PernaEsquerdaQualidadeMuscular,BracoDireitoNivelGordura,BracoDireitoMassaAdiposa,BracoDireitoMassaNaoAdiposa,BracoDireitoMassaMuscular,BracoDireitoQualidadeMuscular,BracoEsquerdoNivelGordura,BracoEsquerdoMassaAdiposa,BracoEsquerdoMassaNaoAdiposa,BracoEsquerdoMassaMuscular,BracoEsquerdoQualidadeMuscular,TroncoNivelGordura,TroncoMassaAdiposa,TroncoMassaNaoAdiposa,TroncoMassaMuscular,DataRegistroBalanca,DataAtualizacao,PacienteId,Id")] HistoricoPaciente historicoPaciente)
        {
            if (id != historicoPaciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    historicoPaciente.DataAtualizacao = DateTime.Now;
                    _context.Update(historicoPaciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoPacienteExists(historicoPaciente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { Id = historicoPaciente.PacienteId });
            }

            ViewBag.Paciente = _context.Pacientes.Find(historicoPaciente.PacienteId).Nome;
            ViewBag.PacienteId = historicoPaciente.PacienteId;

            return View(historicoPaciente);
        }

        // GET: HistoricoPacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoPaciente = await _context.HistoricoPacientes
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historicoPaciente == null)
            {
                return NotFound();
            }

            return View(historicoPaciente);
        }

        // POST: HistoricoPacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicoPaciente = await _context.HistoricoPacientes.FindAsync(id);
            _context.HistoricoPacientes.Remove(historicoPaciente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { Id = historicoPaciente.PacienteId });
        }

        private bool HistoricoPacienteExists(int id)
        {
            return _context.HistoricoPacientes.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Importar(int PacienteId, IFormFile File)
        {
            int counter = 0;
            int saveCounter = 0;
            int errorCounter = 0;

            if (File != null)
            {
                using (var reader = new StreamReader(File.OpenReadStream()))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var historico = new HistoricoPaciente();

                        if (File.FileName.ToLower().Contains("html"))
                        {
                            var data = line.Split("<br>");
                            string pattern = @"[^,^.^\d]";
                            string replacement = string.Empty;
                            
                            //*** Dados Gerais
                            var peso = Convert.ToDecimal(Regex.Replace(data[2].Replace(",", "."), pattern, replacement));
                            var nivelGordura = Convert.ToDecimal(Regex.Replace(data[4].Replace(",", "."), pattern, replacement));
                            var massaAdiposa = peso * nivelGordura / 100;
                            var massaNaoAdiposa = peso - massaAdiposa;
                            historico.PacienteId = PacienteId;
                            historico.Peso = peso;
                            historico.NivelGordura = nivelGordura;
                            historico.AguaCorporal = Convert.ToDecimal(Regex.Replace(data[26].Replace(",", "."), pattern, replacement));
                            historico.GorduraVisceral = Convert.ToDecimal(Regex.Replace(data[23].Replace(",", "."), pattern, replacement));
                            historico.MassaMuscular = Convert.ToDecimal(Regex.Replace(data[10].Replace(",", "."), pattern, replacement));
                            historico.MassaOssea = Convert.ToDecimal(Regex.Replace(data[22].Replace(",", "."), pattern, replacement));
                            historico.IdadeMetabolica = Convert.ToInt32(Regex.Replace(data[25].Replace(",", "."), pattern, replacement));
                            historico.MassaAdiposa = massaAdiposa; // Calculo 
                            historico.MassaNaoAdiposa = massaNaoAdiposa; // Calculo 
                            historico.IngestaoCalorica = 0;
                            historico.TaxaMetabolicaBasal = Convert.ToDecimal(Regex.Replace(data[24].Replace(",", "."), pattern, replacement).Substring(0, Regex.Replace(data[24].Replace(",", "."), pattern, replacement).Length/2)); //TODO remover duplicidade
                            historico.MassaCorporal = Convert.ToDecimal(Regex.Replace(data[2].Replace(",", "."), pattern, replacement));
                            historico.QualidadeMuscularTotal = Convert.ToDecimal(Regex.Replace(data[16].Replace(",", "."), pattern, replacement));

                            //*** Perna Direita
                            var gorduraPernaDireita = Convert.ToDecimal(Regex.Replace(data[8].Replace(",", "."), pattern, replacement));
                            var massaAdiposaPernaDireita = (peso * 18.15m / 100) * gorduraPernaDireita / 100;
                            historico.PernaDireitaNivelGordura = gorduraPernaDireita;
                            historico.PernaDireitaMassaAdiposa = massaAdiposaPernaDireita; // Calculo 
                            historico.PernaDireitaMassaNaoAdiposa = (peso * 18.15m / 100) - massaAdiposaPernaDireita; // Calculo 
                            historico.PernaDireitaMassaMuscular = Convert.ToDecimal(Regex.Replace(data[14].Replace(",", "."), pattern, replacement));
                            historico.PernaDireitaQualidadeMuscular = Convert.ToDecimal(Regex.Replace(data[20].Replace(",", "."), pattern, replacement));

                            //*** Perna Esquerda
                            var gorduraPernaEsquerda = Convert.ToDecimal(Regex.Replace(data[7].Replace(",", "."), pattern, replacement));
                            var massaAdiposaPernaEsquerda = (peso * 18.15m / 100) * gorduraPernaEsquerda / 100;
                            historico.PernaEsquerdaNivelGordura = gorduraPernaEsquerda;
                            historico.PernaEsquerdaMassaAdiposa = massaAdiposaPernaEsquerda; //Calculo
                            historico.PernaEsquerdaMassaNaoAdiposa = (peso * 18.15m / 100) - massaAdiposaPernaEsquerda; //Calculo
                            historico.PernaEsquerdaMassaMuscular = Convert.ToDecimal(Regex.Replace(data[13].Replace(",", "."), pattern, replacement));
                            historico.PernaEsquerdaQualidadeMuscular = Convert.ToDecimal(Regex.Replace(data[19].Replace(",", "."), pattern, replacement));

                            //*** Braço Direito
                            var gorduraBracoDireito = Convert.ToDecimal(Regex.Replace(data[6].Replace(",", "."), pattern, replacement));
                            var massaAdiposaBracoDireito = (peso * 4.96m / 100) * gorduraBracoDireito / 100;
                            historico.BracoDireitoNivelGordura = gorduraBracoDireito;
                            historico.BracoDireitoMassaAdiposa = massaAdiposaBracoDireito; //Calculo
                            historico.BracoDireitoMassaNaoAdiposa = (peso * 4.96m / 100) - massaAdiposaBracoDireito; //Calculo
                            historico.BracoDireitoMassaMuscular = Convert.ToDecimal(Regex.Replace(data[12].Replace(",", "."), pattern, replacement));
                            historico.BracoDireitoQualidadeMuscular = Convert.ToDecimal(Regex.Replace(data[18].Replace(",", "."), pattern, replacement));

                            //*** Braço Esquerdo
                            var gorduraBracoEsquerdo = Convert.ToDecimal(Regex.Replace(data[5].Replace(",", "."), pattern, replacement));
                            var massaAdiposaBracoEsquerdo = (peso * 4.96m / 100) * gorduraBracoEsquerdo / 100;
                            historico.BracoEsquerdoNivelGordura = gorduraBracoEsquerdo;
                            historico.BracoEsquerdoMassaAdiposa = massaAdiposaBracoEsquerdo; //Calculo
                            historico.BracoEsquerdoMassaNaoAdiposa = (peso * 4.96m / 100) - massaAdiposaBracoEsquerdo; //Calculo
                            historico.BracoEsquerdoMassaMuscular = Convert.ToDecimal(Regex.Replace(data[11].Replace(",", "."), pattern, replacement));
                            historico.BracoEsquerdoQualidadeMuscular = Convert.ToDecimal(Regex.Replace(data[17].Replace(",", "."), pattern, replacement));

                            //*** Tronco
                            var gorduraTronco = Convert.ToDecimal(Regex.Replace(data[9].Replace(",", "."), pattern, replacement));
                            var massaAdiposaTronco = (peso * 53.33m / 100) * gorduraTronco / 100;
                            historico.TroncoNivelGordura = gorduraTronco;
                            historico.TroncoMassaAdiposa = massaAdiposaTronco; //Calculo
                            historico.TroncoMassaNaoAdiposa = (peso * 53.33m / 100) - massaAdiposaTronco; //Calculo
                            historico.TroncoMassaMuscular = Convert.ToDecimal(Regex.Replace(data[15].Replace(",", "."), pattern, replacement));
                           
                            //*** Datas
                            historico.DataRegistroBalanca = DateTime.Now;
                            historico.DataAtualizacao = DateTime.Now;

                            counter++;
                        }
                        else if (File.FileName.ToLower().Contains("csv"))
                        {
                            var data = line.Split(",");
                            string pattern = @"[^,^.^\d]";
                            string replacement = string.Empty;

                            if (data.Length > 1)
                            {
                                //*** Dados Gerais
                                var peso = Convert.ToDecimal(Regex.Replace(data[27].Replace(",", "."), pattern, replacement));
                                var nivelGordura = Convert.ToDecimal(Regex.Replace(data[31].Replace(",", "."), pattern, replacement));
                                var massaAdiposa = peso * nivelGordura / 100;
                                var massaNaoAdiposa = peso - massaAdiposa;
                                historico.PacienteId = PacienteId;
                                historico.Peso = peso;
                                historico.NivelGordura = nivelGordura;
                                historico.AguaCorporal = Convert.ToDecimal(Regex.Replace(data[63].Replace(",", "."), pattern, replacement));
                                historico.GorduraVisceral = Convert.ToDecimal(Regex.Replace(data[57].Replace(",", "."), pattern, replacement));
                                historico.MassaMuscular = Convert.ToDecimal(Regex.Replace(data[43].Replace(",", "."), pattern, replacement));
                                historico.MassaOssea = Convert.ToDecimal(Regex.Replace(data[55].Replace(",", "."), pattern, replacement));
                                historico.IdadeMetabolica = Convert.ToInt32(Regex.Replace(data[61].Replace(",", "."), pattern, replacement));
                                historico.MassaAdiposa = massaAdiposa;
                                historico.MassaNaoAdiposa = massaNaoAdiposa;
                                historico.IngestaoCalorica = Convert.ToDecimal(Regex.Replace(data[59].Replace(",", "."), pattern, replacement));
                                historico.TaxaMetabolicaBasal = 0;
                                historico.MassaCorporal = 0;
                                historico.QualidadeMuscularTotal = 0;

                                //*** Perna Direita
                                var gorduraPernaDireita = Convert.ToDecimal(Regex.Replace(data[33].Replace(",", "."), pattern, replacement));
                                var massaAdiposaPernaDireita = (peso * 18.15m / 100) * gorduraPernaDireita / 100;
                                historico.PernaDireitaNivelGordura = gorduraPernaDireita;
                                historico.PernaDireitaMassaAdiposa = massaAdiposaPernaDireita; //Calculo
                                historico.PernaDireitaMassaNaoAdiposa = (peso * 18.15m / 100) - massaAdiposaPernaDireita; //Calculo
                                historico.PernaDireitaMassaMuscular = Convert.ToDecimal(Regex.Replace(data[49].Replace(",", "."), pattern, replacement));
                                historico.PernaDireitaQualidadeMuscular = 0;

                                //*** Perna Esquerda
                                var gorduraPernaEsquerda = Convert.ToDecimal(Regex.Replace(data[39].Replace(",", "."), pattern, replacement));
                                var massaAdiposaPernaEsquerda = (peso * 18.15m / 100) * gorduraPernaEsquerda / 100;
                                historico.PernaEsquerdaNivelGordura = gorduraPernaEsquerda;
                                historico.PernaEsquerdaMassaAdiposa = massaAdiposaPernaEsquerda; //Calculo
                                historico.PernaEsquerdaMassaNaoAdiposa = (peso * 18.15m / 100) - massaAdiposaPernaEsquerda; //Calculo
                                historico.PernaEsquerdaMassaMuscular = Convert.ToDecimal(Regex.Replace(data[51].Replace(",", "."), pattern, replacement));
                                historico.PernaEsquerdaQualidadeMuscular = 0;

                                //*** Braço Direito
                                var gorduraBracoDireito = Convert.ToDecimal(Regex.Replace(data[37].Replace(",", "."), pattern, replacement));
                                var massaAdiposaBracoDireito = (peso * 4.96m / 100) * gorduraBracoDireito / 100;
                                historico.BracoDireitoNivelGordura = gorduraBracoDireito;
                                historico.BracoDireitoMassaAdiposa = massaAdiposaBracoDireito; //Calculo
                                historico.BracoDireitoMassaNaoAdiposa = (peso * 4.96m / 100) - massaAdiposaBracoDireito; //Calculo
                                historico.BracoDireitoMassaMuscular = Convert.ToDecimal(Regex.Replace(data[45].Replace(",", "."), pattern, replacement)); 
                                historico.BracoDireitoQualidadeMuscular = 0;

                                //*** Braço Esquerdo
                                var gorduraBracoEsquerdo = Convert.ToDecimal(Regex.Replace(data[35].Replace(",", "."), pattern, replacement));
                                var massaAdiposaBracoEsquerdo = (peso * 4.96m / 100) * gorduraBracoEsquerdo / 100;
                                historico.BracoEsquerdoNivelGordura = gorduraBracoEsquerdo;
                                historico.BracoEsquerdoMassaAdiposa = massaAdiposaBracoEsquerdo; //Calculo
                                historico.BracoEsquerdoMassaNaoAdiposa = (peso * 4.96m / 100) - massaAdiposaBracoEsquerdo; //Calculo
                                historico.BracoEsquerdoMassaMuscular = Convert.ToDecimal(Regex.Replace(data[47].Replace(",", "."), pattern, replacement));
                                historico.BracoEsquerdoQualidadeMuscular = 0;

                                //*** Tronco
                                var gorduraTronco = Convert.ToDecimal(Regex.Replace(data[41].Replace(",", "."), pattern, replacement));
                                var massaAdiposaTronco = (peso * 53.33m / 100) * gorduraTronco / 100;
                                historico.TroncoNivelGordura = gorduraTronco;
                                historico.TroncoMassaAdiposa = massaAdiposaTronco; //Calculo
                                historico.TroncoMassaNaoAdiposa = (peso * 53.33m / 100) - massaAdiposaTronco; //Calculo
                                historico.TroncoMassaMuscular = Convert.ToDecimal(Regex.Replace(data[53].Replace(",", "."), pattern, replacement));

                                //*** Datas
                                historico.DataRegistroBalanca = Convert.ToDateTime(string.Concat(data[13].Replace("\"", string.Empty), " ", data[15].Replace("\"", string.Empty))); //13 e 15
                                historico.DataAtualizacao = DateTime.Now;

                                counter++;
                            }
                            else
                            {
                                return RedirectToAction("Index", new { Id = PacienteId, mensagem = "Não há dados para serem importados no arquivo selecionado" });
                            }
                        }

                        if (!_context.HistoricoPacientes.Where(m => m.DataRegistroBalanca == historico.DataRegistroBalanca && m.PacienteId == historico.PacienteId).Any())
                        {
                            saveCounter++;
                            _context.Add(historico);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            errorCounter++;
                        }
                    }
                }
            }

            var mensagem = string.Concat(counter, " registros lidos, ", saveCounter, " registros importados", errorCounter > 0 ? string.Concat(", ", errorCounter, " registros não importados") : string.Empty);
            return RedirectToAction("Index", new { Id = PacienteId, mensagem = mensagem });
        }
    }
}
