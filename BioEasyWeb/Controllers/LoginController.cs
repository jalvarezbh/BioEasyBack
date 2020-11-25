using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class LoginController : ApiController
    {
        private Usuario usuario = new Usuario();
       
        [HttpPost]
        public IHttpActionResult ValidarLogin(UsuarioModel loginModel)
        {
            try
            {
                var login = usuario.ValidarLogin(loginModel.Email, loginModel.Senha);               
                return Ok(login);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("login"))
                    return Content(HttpStatusCode.Unauthorized, ex.Message);

                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult LoginSimulado(UsuarioModel loginModel)
        {
            try
            {
                var login = usuario.LoginSimulado(loginModel.Id.ToString(), loginModel.IdSimulado.ToString());
                return Ok(login);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("login"))
                    return Content(HttpStatusCode.Unauthorized, ex.Message);

                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult ValidarTempoAcesso(string id)
        {
            try
            {
                var login = usuario.ValidarTempoAcesso(id);
                return Ok(login);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        public IHttpActionResult EnviarEmailLembrarSenha(string email)
        {
            try
            {
                usuario.EnviarEmailLembrarSenha(email);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}