using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class EmpresaController : ApiController
    {
        private Empresa empresa = new Empresa();
        private Endereco endereco = new Endereco();

        [ActionName("Listar")]
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                var retorno = empresa.Listar();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [ActionName("ListarAtivos")]
        [HttpGet]
        public IHttpActionResult ListarAtivos()
        {
            try
            {
                var retorno = empresa.ListarAtivos();
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
                var retorno = empresa.Buscar(id);
                var endRegistro = endereco.BuscarEmpresa(retorno);
                                
                return Ok(endRegistro);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(EmpresaModel registro)
        {
            try
            {
                var endRegistro = endereco.GravarEmpresa(registro);
                registro.IdEndereco = endRegistro.Id;
                var retorno = empresa.Gravar(registro);
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
                empresa.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}