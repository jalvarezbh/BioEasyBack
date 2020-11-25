using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class PacienteController : ApiController
    {
        private Paciente paciente = new Paciente();
        private Endereco endereco = new Endereco();

        [ActionName("Listar")]
        [HttpGet]
        public IHttpActionResult Listar(string idUsuario, string idEmpresa)
        {
            try
            {
                var retorno = paciente.Listar(idUsuario, idEmpresa);
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
                var retorno = paciente.Buscar(id);
                var endRegistro = endereco.BuscarPaciente(retorno);

                return Ok(endRegistro);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(PacienteModel registro)
        {
            try
            {
                var endRegistro = endereco.GravarPaciente(registro);
                registro.IdEndereco = endRegistro.Id;
                var retorno = paciente.Gravar(registro);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [ActionName("Importar")]
        [HttpPost]
        public IHttpActionResult Importar(PacienteModel registro)
        {
            try
            {               
                var retorno = paciente.Gravar(registro);
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
                paciente.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}