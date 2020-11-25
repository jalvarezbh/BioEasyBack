using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class SistemaController : ApiController
    {
        private Sistema sistema = new Sistema();

        [ActionName("Buscar")]
        [HttpGet]
        public IHttpActionResult Buscar()
        {
            try
            {
                var retorno = sistema.Buscar();
                
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(SistemaModel registro)
        {
            try
            {
                var retorno = sistema.Incluir(registro);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}