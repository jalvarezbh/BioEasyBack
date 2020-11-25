using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class LaboratorialController : ApiController
    {
        private Laboratorial laboratorial = new Laboratorial();

        [ActionName("Listar")]
        [HttpGet]
        public IHttpActionResult Listar(string idPaciente)
        {
            try
            {
                var retorno = laboratorial.Listar(idPaciente);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [ActionName("Buscar")]
        [HttpGet]
        public IHttpActionResult Buscar(string id)
        {
            try
            {
                var retorno = laboratorial.Buscar(id);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(LaboratorialModel registro)
        {
            try
            {
                var retorno = laboratorial.Gravar(registro);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}