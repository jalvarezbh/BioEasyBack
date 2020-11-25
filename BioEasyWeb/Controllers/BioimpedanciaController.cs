using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class BioimpedanciaController : ApiController
    {
        private Bioimpedancia bioimpedancia = new Bioimpedancia();       

        [ActionName("Listar")]
        [HttpGet]
        public IHttpActionResult Listar(string idPaciente)
        {
            try
            {
                var retorno = bioimpedancia.Listar(idPaciente);
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
                var retorno = bioimpedancia.Buscar(id);                

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(BioimpedanciaModel registro)
        {
            try
            {
                var retorno = bioimpedancia.Gravar(registro);
                return Ok(retorno);               
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Delete")]
        [HttpGet]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                bioimpedancia.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

    }
}