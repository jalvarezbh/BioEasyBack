using BioEasyBase;
using BioEasyBase.Model;
using System;
using System.Net;
using System.Web.Http;

namespace BioEasyWeb.Controllers
{
    public class UsuarioController : ApiController
    {
        private Autonomo autonomo = new Autonomo();        
        private Endereco endereco = new Endereco();
        private Usuario usuario = new Usuario();


        [ActionName("Listar")]
        [HttpGet]
        public IHttpActionResult Listar()
        {
            try
            {
                var retorno = usuario.Listar();
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [ActionName("ListarInativos")]
        [HttpGet]
        public IHttpActionResult ListarInativos()
        {
            try
            {
                var retorno = usuario.ListarInativos();
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
                var retorno = usuario.Buscar(id);
                //var endRegistro = endereco.BuscarEmpresa(retorno);

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Gravar")]
        [HttpPost]
        public IHttpActionResult Gravar(UsuarioCompletoModel registro)
        {
            try
            {
                if (string.IsNullOrEmpty(registro.EmpresaSolicitada) && 
                    (string.IsNullOrEmpty(registro.IdEmpresa.ToString()) || registro.IdEmpresa.ToString().Equals("00000000-0000-0000-0000-000000000000")))
                {
                    AutonomoModel autonomoModel = new AutonomoModel();
                    autonomoModel.PreencherAutonomo(registro);

                    var endRegistro = endereco.GravarAutonomo(autonomoModel);
                    autonomoModel.IdEndereco = endRegistro.Id;

                    var autonomoRetorno = autonomo.Gravar(autonomoModel);
                    registro.IdAutonomo = autonomoRetorno.Id;
                }                

                UsuarioModel usuarioModel = new UsuarioModel();
                usuarioModel.PreencherUsuario(registro);

                var retorno = usuario.Gravar(usuarioModel);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("IncluirSolicitacao")]
        [HttpPost]
        public IHttpActionResult IncluirSolicitacao(UsuarioCompletoModel registro)
        {
            try
            {
                if (string.IsNullOrEmpty(registro.EmpresaSolicitada) && 
                    (string.IsNullOrEmpty(registro.IdEmpresa.ToString()) || registro.IdEmpresa.ToString().Equals("00000000-0000-0000-0000-000000000000")))
                {
                    AutonomoModel autonomoModel = new AutonomoModel();
                    autonomoModel.PreencherAutonomo(registro);

                    var endRegistro = endereco.GravarAutonomo(autonomoModel);
                    autonomoModel.IdEndereco = endRegistro.Id;

                    var autonomoRetorno = autonomo.Gravar(autonomoModel);
                    registro.IdAutonomo = autonomoRetorno.Id;
                }

                UsuarioModel usuarioModel = new UsuarioModel();
                usuarioModel.PreencherUsuario(registro);
                usuarioModel.Senha = registro.Senha;
                var retorno = usuario.IncluirSolicitacao(usuarioModel);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Ativar")]
        [HttpPost]
        public IHttpActionResult Ativar(UsuarioModel registro)
        {
            try
            {  
                usuario.Ativar(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("Desativar")]
        [HttpPost]
        public IHttpActionResult Desativar(UsuarioModel registro)
        {
            try
            {
                usuario.Desativar(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("AlterarSenhaUsuario")]
        [HttpPost]
        public IHttpActionResult AlterarSenhaUsuario(SenhaModel registro)
        {
            try
            {
                usuario.AlterarSenhaUsuario(registro);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [ActionName("AlterarSenhaUsuarioToken")]
        [HttpPost]
        public IHttpActionResult AlterarSenhaUsuarioToken(SenhaModel registro)
        {
            try
            {
                usuario.AlterarSenhaUsuarioToken(registro);
                return Ok();
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
                usuario.Deletar(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}