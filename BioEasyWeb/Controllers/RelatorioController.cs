using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class RelatorioController : ApiController
    {
        private Relatorio relatorio = new Relatorio();

        [ActionName("Prevencao")]
        [HttpGet]
        public IHttpActionResult Prevencao(string idPaciente)
        {
            try
            {
                var retorno = relatorio.Prevencao(idPaciente);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("PrevencaoLaboratorial")]
        [HttpGet]
        public IHttpActionResult PrevencaoLaboratorial(string idPaciente, DateTime dataAvaliacao)
        {
            try
            {
                var retorno = relatorio.PrevencaoLaboratorial(idPaciente, dataAvaliacao);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("AtualProgresso")]
        [HttpGet]
        public IHttpActionResult AtualProgresso(string idPaciente)
        {
            try
            {
                var retorno = relatorio.AtualProgresso(idPaciente);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("PrevencaoDatas")]
        [HttpGet]
        public IHttpActionResult PrevencaoDatas(string idPaciente)
        {
            try
            {
                var retorno = relatorio.PrevencaoDatas(idPaciente);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("LaboratorialDatas")]
        [HttpGet]
        public IHttpActionResult LaboratorialDatas(string idPaciente)
        {
            try
            {
                var retorno = relatorio.LaboratorialDatas(idPaciente);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("EnviarEmail")]
        [HttpPost]
        public IHttpActionResult EnviarEmail(RelatorioEnvioModel registro)
        {
            try
            {                
                relatorio.EnviarEmail(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

    }
}