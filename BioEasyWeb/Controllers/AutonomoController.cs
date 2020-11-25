using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class AutonomoController : ApiController
    {
        private Autonomo autonomo = new Autonomo();
        private Endereco endereco = new Endereco();

        [ActionName("Listar")]
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                var retorno = autonomo.Listar();
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
                var retorno = autonomo.Buscar(id);
                var endRegistro = endereco.BuscarAutonomo(retorno);

                return Ok(endRegistro);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(AutonomoModel registro)
        {
            try
            {
                var endRegistro = endereco.GravarAutonomo(registro);
                registro.IdEndereco = endRegistro.Id;
                var retorno = autonomo.Gravar(registro);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}