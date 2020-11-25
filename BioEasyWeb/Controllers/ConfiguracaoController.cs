using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class ConfiguracaoController : ApiController
    {
        private Configuracao configuracao = new Configuracao();

        [ActionName("Buscar")]
        [HttpGet]
        public IHttpActionResult Buscar(string idUsuario)
        {
            try
            {
                var retorno = configuracao.Buscar(idUsuario);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(ConfiguracaoModel registro)
        {
            try
            {                
                configuracao.Alterar(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}