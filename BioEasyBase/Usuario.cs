using BioEasyBase.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;

namespace BioEasyBase
{
    public class Usuario : Base
    {
        public UsuarioModel ValidarLogin(string email, string senha)
        {
            try
            {
                UsuarioModel login = new UsuarioModel();

                byte[] salt = new byte[128 / 8];
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(senha, salt);
                string hashed = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256 / 8));

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"SELECT U.ID, 
                                             U.NOME, 
                                             U.EMAIL, 
                                             U.ATIVO,
                                             U.ADMINISTRADOR,
                                             U.ID_EMPRESA,
                                             U.ID_AUTONOMO,
                                             U.DATA_LIMITE,
                                        CASE WHEN E.BALANCA IS NULL
                                              THEN A.BALANCA
                                              ELSE E.BALANCA
                                        END AS BALANCA
                                       FROM USUARIO U
                                       LEFT JOIN EMPRESA E ON E.ID = U.ID_EMPRESA 
                                       LEFT JOIN AUTONOMO A ON A.ID = U.ID_AUTONOMO 
                                       WHERE U.EMAIL = '{email}' AND U.SENHA = '{hashed}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("E-mail ou Senha Inválido");

                            while (reader.Read())
                            {
                                login.Id = Guid.Parse(reader["ID"].ToString());
                                login.Nome = reader["NOME"].ToString();
                                login.Email = reader["EMAIL"].ToString();
                                login.Administrador = Convert.ToBoolean(reader["ADMINISTRADOR"].ToString());
                                login.Balanca = reader["BALANCA"].ToString();

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    login.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_AUTONOMO"].ToString()))
                                {
                                    login.IdAutonomo = Guid.Parse(reader["ID_AUTONOMO"].ToString());
                                }

                                if (Convert.ToDateTime(reader["DATA_LIMITE"].ToString()) < DateTime.Now)
                                {
                                    InativarUsuario(login.Id.ToString());
                                    var dadosConta = new Sistema().BuscarEmailSistema();
                                    throw new Exception("Usuário com login bloqueado entre em contato com administrador pelo e-mail " + dadosConta.Email+ ". Caso seja seu primeiro acesso aguarde a ativação pela nossa equipe de suporte." );
                                }

                                if (!Convert.ToBoolean(reader["ATIVO"].ToString()))
                                {
                                    var dadosConta = new Sistema().BuscarEmailSistema();
                                    throw new Exception("Usuário com login bloqueado entre em contato com administrador pelo e-mail " + dadosConta.Email + ". Caso seja seu primeiro acesso aguarde a ativação pela nossa equipe de suporte.");
                                }

                                AlterarUltimoAcesso(login.Id.ToString());
                            }

                            return login;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioModel LoginSimulado(string idUsuario, string idSimulado)
        {
            try
            {
                UsuarioModel administrador = new UsuarioModel();

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"SELECT U.ID,                                             
                                             U.ATIVO,
                                             U.ADMINISTRADOR                                          
                                       FROM USUARIO U                                      
                                       WHERE U.ID = '{idUsuario}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Usuário não tem permissão para essa funcionalidade");

                            while (reader.Read())
                            {
                                administrador.Id = Guid.Parse(reader["ID"].ToString());
                                administrador.Administrador = Convert.ToBoolean(reader["ADMINISTRADOR"].ToString());
                                administrador.Ativo = Convert.ToBoolean(reader["ATIVO"].ToString());                               
                            }
                        }
                    }
                }

                if (administrador.Administrador && administrador.Ativo)
                {
                    UsuarioModel login = new UsuarioModel();

                    using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                    {
                        connection.Open();

                        String query = $@"SELECT U.ID, 
                                             U.NOME, 
                                             U.EMAIL, 
                                             U.ATIVO,
                                             U.ADMINISTRADOR,
                                             U.ID_EMPRESA,
                                             U.ID_AUTONOMO,
                                             U.DATA_LIMITE,
                                        CASE WHEN E.BALANCA IS NULL
                                              THEN A.BALANCA
                                              ELSE E.BALANCA
                                        END AS BALANCA
                                       FROM USUARIO U
                                       LEFT JOIN EMPRESA E ON E.ID = U.ID_EMPRESA 
                                       LEFT JOIN AUTONOMO A ON A.ID = U.ID_AUTONOMO 
                                       WHERE U.ID = '{idSimulado}'";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                            {
                                if (!reader.HasRows)
                                    throw new Exception("E-mail ou Senha Inválido");

                                while (reader.Read())
                                {
                                    login.Id = Guid.Parse(reader["ID"].ToString());
                                    login.Nome = reader["NOME"].ToString();
                                    login.Email = reader["EMAIL"].ToString();
                                    login.Administrador = Convert.ToBoolean(reader["ADMINISTRADOR"].ToString());
                                    login.Balanca = reader["BALANCA"].ToString();

                                    if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                    {
                                        login.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                    }

                                    if (!string.IsNullOrEmpty(reader["ID_AUTONOMO"].ToString()))
                                    {
                                        login.IdAutonomo = Guid.Parse(reader["ID_AUTONOMO"].ToString());
                                    }

                                    if (Convert.ToDateTime(reader["DATA_LIMITE"].ToString()) < DateTime.Now)
                                    {
                                        InativarUsuario(login.Id.ToString());
                                        var dadosConta = new Sistema().BuscarEmailSistema();
                                        throw new Exception("Usuário com login bloqueado entre em contato com administrador pelo e-mail " + dadosConta.Email + ". Caso seja seu primeiro acesso aguarde a ativação pela nossa equipe de suporte.");
                                    }

                                    if (!Convert.ToBoolean(reader["ATIVO"].ToString()))
                                    {
                                        var dadosConta = new Sistema().BuscarEmailSistema();
                                        throw new Exception("Usuário com login bloqueado entre em contato com administrador pelo e-mail " + dadosConta.Email + ". Caso seja seu primeiro acesso aguarde a ativação pela nossa equipe de suporte.");
                                    }

                                    AlterarUltimoAcesso(login.Id.ToString());
                                }

                                return login;
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Usuário não tem permissão para essa funcionalidade");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string DecryptString(string encrString)
        {
            byte[] b;
            string decrypted;
            try
            {
                b = Convert.FromBase64String(encrString);
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);
            }
            catch (FormatException fe)
            {
                decrypted = "";
            }
            return decrypted;
        }

        public string EncryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public void ValidarEmail(string email)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = "SELECT ID FROM USUARIO WHERE EMAIL = '{0}'";
                    query = string.Format(query, email);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.HasRows)
                                throw new Exception("E-mail já utilizado por outro usuário.");

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void InativarUsuario(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE USUARIO 
                                       SET ATIVO = 0
                                       WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarUltimoAcesso(string id)
        {
            try
            {
                string dataUltimoAcesso = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE USUARIO 
                                       SET ULTIMO_LOGIN = '{dataUltimoAcesso}'
                                       WHERE ATIVO = 1 
                                         AND ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarSenhaUsuario(SenhaModel senha)
        {
            try
            {
                if (senha.SenhaNova != senha.SenhaRepetir)
                    throw new Exception("Campo Confirmar Senha Nova diferente do Campo Senha Nova.");

                byte[] salt = new byte[128 / 8];
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(senha.SenhaNova, salt);
                string hashedNova = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256 / 8));

                Rfc2898DeriveBytes rfc2898DeriveBytesAtual = new Rfc2898DeriveBytes(senha.SenhaAtual, salt);
                string hashedAtual = Convert.ToBase64String(rfc2898DeriveBytesAtual.GetBytes(256 / 8));

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE USUARIO 
                                       SET SENHA = '{hashedNova}'
                                       WHERE ATIVO = 1 
                                         AND ID = '{senha.Id}'
                                         AND SENHA = '{hashedAtual}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AlterarSenhaUsuarioToken(SenhaModel senha)
        {
            try
            {
                if (senha.SenhaNova != senha.SenhaRepetir)
                    throw new Exception("Campo Confirmar Senha Nova diferente do Campo Senha Nova.");

                byte[] salt = new byte[128 / 8];
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(senha.SenhaNova, salt);
                string hashedNova = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256 / 8));

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE USUARIO 
                                       SET SENHA = '{hashedNova}'
                                       WHERE ATIVO = 1 
                                         AND ID = '{senha.Id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioModel> Listar()
        {
            try
            {
                List<UsuarioModel> usuarios = new List<UsuarioModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM USUARIO";
                    query += $"  ORDER BY NOME";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsuarioModel usuario = new UsuarioModel();
                                usuario.Id = Guid.Parse(reader["ID"].ToString());
                                usuario.Nome = reader["NOME"].ToString();
                                usuario.Celular = reader["CELULAR"].ToString();
                                usuario.Email = reader["EMAIL"].ToString();
                                usuario.DataLimite = Convert.ToDateTime(reader["DATA_LIMITE"].ToString());
                                usuario.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                                usuarios.Add(usuario);
                            }
                            return usuarios;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioModel> ListarInativos()
        {
            try
            {
                string dataLimite = DateTime.Now.ToString("yyyy-MM-dd");
                List<UsuarioModel> usuarios = new List<UsuarioModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM USUARIO
                                        WHERE ATIVO = 0 
                                        OR DATA_LIMITE < '{dataLimite}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsuarioModel usuario = new UsuarioModel();
                                usuario.Id = Guid.Parse(reader["ID"].ToString());
                                usuario.Nome = reader["NOME"].ToString();
                                usuario.Celular = reader["CELULAR"].ToString();
                                usuario.Email = reader["EMAIL"].ToString();
                                usuario.DataLimite = Convert.ToDateTime(reader["DATA_LIMITE"].ToString());
                                usuario.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                                usuario.EmpresaSolicitada = reader["EMPRESA_SOLICITADA"].ToString();
                                usuarios.Add(usuario);
                            }
                            return usuarios;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioModel Buscar(string id)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM USUARIO
                                        WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe usuario com esse identificador.");

                            while (reader.Read())
                            {
                                usuario.Id = Guid.Parse(reader["ID"].ToString());
                                usuario.Nome = reader["NOME"].ToString();
                                usuario.Email = reader["EMAIL"].ToString();
                                usuario.CPF = reader["CPF"].ToString();
                                usuario.Celular = reader["CELULAR"].ToString();
                                usuario.Documento = reader["DOCUMENTO"].ToString();
                                usuario.Especialidade = reader["ESPECIALIDADE"].ToString();
                                usuario.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                                usuario.Administrador = Convert.ToBoolean(reader["ADMINISTRADOR"]);
                                usuario.DataLimite = Convert.ToDateTime(reader["DATA_LIMITE"].ToString());
                                usuario.EmpresaSolicitada = reader["EMPRESA_SOLICITADA"].ToString();

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    usuario.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_AUTONOMO"].ToString()))
                                {
                                    usuario.IdAutonomo = Guid.Parse(reader["ID_AUTONOMO"].ToString());
                                }
                            }

                            return usuario;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioModel Gravar(UsuarioModel usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Id.ToString()) || usuario.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(usuario);
                    return registro;
                }
                else
                {
                    Alterar(usuario);
                }

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioModel Incluir(UsuarioModel usuario)
        {
            try
            {
                String celular = Regex.Replace(usuario.Celular, @"[^\w\d]", "");
                usuario.Celular = celular;
                string dataLimite = usuario.DataLimite.ToString("yyyy-MM-dd");
                string ultimoLogin = DateTime.Now.ToString("yyyy-MM-dd");
                byte[] salt = new byte[128 / 8];
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes("BioEasy2020", salt);
                string hashed = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256 / 8));

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();
                    
                    String query = $@" INSERT INTO USUARIO ( 
                                       NOME,              
                                       EMAIL,
                                       CPF,
                                       CELULAR,
                                       DOCUMENTO,
                                       ESPECIALIDADE,
                                       ATIVO,   
                                       ADMINISTRADOR,
                                       DATA_LIMITE,
                                       SENHA,
                                       ID_EMPRESA,
                                       EMPRESA_SOLICITADA,
                                       ID_AUTONOMO,
                                       ULTIMO_LOGIN )  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{usuario.Nome}',
                                       '{usuario.Email}',
                                       '{usuario.CPF}',
                                       '{usuario.Celular}',
                                       '{usuario.Documento}',
                                       '{usuario.Especialidade}',
                                       '{usuario.Ativo}',
                                       '{usuario.Administrador}',
                                       '{dataLimite}',  
                                       '{hashed}',
                                       '{usuario.IdEmpresa}',
                                       '{usuario.EmpresaSolicitada}',
                                       '{usuario.IdAutonomo}',
                                       '{ultimoLogin}' )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        usuario.Id = (Guid)command.ExecuteScalar();
                    }

                    Configuracao configuracao = new Configuracao();
                    configuracao.Incluir(usuario.Id);
                }

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(UsuarioModel usuario)
        {
            try
            {
                String celular = Regex.Replace(usuario.Celular, @"[^\w\d]", "");
                usuario.Celular = celular;
                string dataLimite = usuario.DataLimite.ToString("yyyy-MM-dd");

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE USUARIO SET 
                                       NOME = '{usuario.Nome}',              
                                       EMAIL =  '{usuario.Email}', 
                                       CPF =  '{usuario.CPF}', 
                                       CELULAR = '{usuario.Celular}',           
                                       DOCUMENTO = '{usuario.Documento}',   
                                       ESPECIALIDADE = '{usuario.Especialidade}',   
                                       ATIVO = '{usuario.Ativo}',   
                                       ADMINISTRADOR = '{usuario.Administrador}',   
                                       DATA_LIMITE = '{dataLimite}',   
                                       ID_EMPRESA = '{usuario.IdEmpresa}', 
                                       EMPRESA_SOLICITADA = '{usuario.EmpresaSolicitada}',   
                                       ID_AUTONOMO = '{usuario.IdAutonomo}'
                                       WHERE ID = '{usuario.Id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Deletar(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  DELETE
                                        FROM USUARIO
                                        WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioModel IncluirSolicitacao(UsuarioModel usuario)
        {
            try
            {
                ValidarEmail(usuario.Email);

                String celular = Regex.Replace(usuario.Celular, @"[^\w\d]", "");
                usuario.Celular = celular;
                string dataLimite = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                usuario.Ativo = false;
                string ultimoLogin = DateTime.Now.ToString("yyyy-MM-dd");
                byte[] salt = new byte[128 / 8];
                Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(usuario.Senha, salt);
                string hashed = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256 / 8));

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO USUARIO ( 
                                       NOME,              
                                       EMAIL,
                                       CPF,
                                       CELULAR,
                                       DOCUMENTO,
                                       ESPECIALIDADE,
                                       ATIVO,   
                                       ADMINISTRADOR,
                                       DATA_LIMITE,
                                       SENHA,
                                       ID_EMPRESA,
                                       EMPRESA_SOLICITADA,
                                       ID_AUTONOMO,
                                       ULTIMO_LOGIN )  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{usuario.Nome}',
                                       '{usuario.Email}',
                                       '{usuario.CPF}',
                                       '{usuario.Celular}',
                                       '{usuario.Documento}',
                                       '{usuario.Especialidade}',
                                       '{usuario.Ativo}',
                                       '{usuario.Administrador}',
                                       '{dataLimite}',  
                                       '{hashed}',
                                       '{usuario.IdEmpresa}',
                                       '{usuario.EmpresaSolicitada}',
                                       '{usuario.IdAutonomo}',
                                       '{ultimoLogin}' )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        usuario.Id = (Guid)command.ExecuteScalar();
                    }

                    Configuracao configuracao = new Configuracao();
                    configuracao.Incluir(usuario.Id);
                    EnviarEmailNovoUsuario(usuario.Email);
                }

                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Ativar(UsuarioModel usuario)
        {
            try
            {
                var usuarioValida = Buscar(usuario.Id.ToString());
                if(!string.IsNullOrEmpty(usuarioValida.EmpresaSolicitada) 
                    && usuarioValida.IdEmpresa.ToString().Equals("00000000-0000-0000-0000-000000000000") 
                    && usuarioValida.IdAutonomo.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    throw new Exception("Este usuário precisa ser relacionado a uma empresa para ser ativado.");
                }

                string dataLimite = usuario.DataLimite.ToString("yyyy-MM-dd");

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE USUARIO SET 
                                       ATIVO = 1,   
                                       DATA_LIMITE = '{dataLimite}'
                                       WHERE ID = '{usuario.Id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                EnviarEmailUsuarioAtivo(usuario.Id.ToString());

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Desativar(UsuarioModel usuario)
        {
            try
            {
                string dataLimite = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE USUARIO SET 
                                       ATIVO = 0,   
                                       DATA_LIMITE = '{dataLimite}'
                                       WHERE ID = '{usuario.Id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EnviarEmailNovoUsuario(string email)
        {
            UsuarioModel usuario = new UsuarioModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = "SELECT ID, NOME, EMAIL FROM USUARIO WHERE EMAIL = '{0}' AND ATIVO = 0";
                    query = string.Format(query, email);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("E-mail não cadastrado.");

                            while (reader.Read())
                            {
                                usuario.Id = Guid.Parse(reader["ID"].ToString());
                                usuario.Nome = reader["NOME"].ToString();
                                usuario.Email = reader["EMAIL"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                string path = HttpContext.Current.Server.MapPath("\\Template.html");
                string html = string.Empty;
                var dadosConta = new Sistema().BuscarEmailSistema();
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(dadosConta.Email);

                mailMessage.Subject = "Solicitação BioEasy";
                using (var arquivoHtml = new StreamReader(path))
                {
                    html = arquivoHtml.ReadToEnd();
                    html = html.Replace("[TITULO]", @"Seja Bem Vindo ao BioEasy.");
                    html = html.Replace("[SAUDACAO]", @"Prezado(a)," + usuario.Nome);
                    string linha1 = Linha(true, "Recebemos a sua solicitação de acesso.", null);
                    string linha2 = Linha(true, "Nossa equipe esta analisando e enviaremos um e-mail com o retorno.", null);
                    html = html.Replace("[LINHA]", linha1 + linha2);
                }

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = html;
                mailMessage.To.Add(email);

                var smtp = ConfiguracaoSMTP();

                smtp.Send(mailMessage);
                smtp.Dispose();
            }
            catch (Exception)
            {
                return;
            }

        }

        public void EnviarEmailUsuarioAtivo(string id)
        {
            UsuarioModel usuario = new UsuarioModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = "SELECT ID, NOME, EMAIL, DATA_LIMITE FROM USUARIO WHERE ID = '{0}' AND ATIVO = 1";
                    query = string.Format(query, id);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("E-mail não ativado.");

                            while (reader.Read())
                            {
                                usuario.Id = Guid.Parse(reader["ID"].ToString());
                                usuario.Nome = reader["NOME"].ToString();
                                usuario.Email = reader["EMAIL"].ToString();
                                usuario.DataLimite = Convert.ToDateTime(reader["DATA_LIMITE"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                string path = HttpContext.Current.Server.MapPath("\\Template.html");
                string html = string.Empty;
                var dadosConta = new Sistema().BuscarEmailSistema();
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(dadosConta.Email);

                mailMessage.Subject = "BioEasy";
                using (var arquivoHtml = new StreamReader(path))
                {
                    html = arquivoHtml.ReadToEnd();
                    html = html.Replace("[TITULO]", @"Seja Bem Vindo ao BioEasy.");
                    html = html.Replace("[SAUDACAO]", @"Prezado(a)," + usuario.Nome);
                    string linha1 = Linha(true, "Sua solicitação de acesso foi aceita.", null);
                    string linha2 = Linha(true, "Já pode utilizar o nosso sistema, seu acesso está liberado até " + usuario.DataLimite.ToString("dd/MM/yyyy") + ".", null);
                    string linha3 = Linha(true, "Qualquer dúvida estamos a disposição.", null);
                    string linha4 = Linha(false, "Click e faça seu login.", $"https://bioeasypro.club/login");
                    html = html.Replace("[LINHA]", linha1 + linha2 + linha3 + linha4);
                }
                //seu sistema já está ativo e pronto para usar. Seu acesso online está liberado até xx/xx/xxxx
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = html;
                mailMessage.To.Add(usuario.Email);

                var smtp = ConfiguracaoSMTP();

                smtp.Send(mailMessage);
                smtp.Dispose();
            }
            catch (Exception)
            {
                return;
            }
        }

        public void EnviarEmailLembrarSenha(string email)
        {
            UsuarioModel usuario = new UsuarioModel();

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = "SELECT ID, NOME, EMAIL, SENHA FROM USUARIO WHERE EMAIL = '{0}' AND ATIVO = 1";
                    query = string.Format(query, email);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("E-mail não cadastrado.");

                            while (reader.Read())
                            {
                                usuario.Id = Guid.Parse(reader["ID"].ToString());
                                usuario.Nome = reader["NOME"].ToString();
                                usuario.Email = reader["EMAIL"].ToString();
                                usuario.Senha = reader["SENHA"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                string path = HttpContext.Current.Server.MapPath("\\Template.html");
                string html = string.Empty;
                var dadosConta = new Sistema().BuscarEmailSistema();
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(dadosConta.Email);

                mailMessage.Subject = "BioEasy Redefinir Senha";
                using (var arquivoHtml = new StreamReader(path))
                {
                    html = arquivoHtml.ReadToEnd();
                    html = html.Replace("[TITULO]", @"Solicitação para redefinir a senha de acesso.");
                    html = html.Replace("[SAUDACAO]", @"Prezado(a)," + usuario.Nome);


                    string linha1 = Linha(false, "Para redefinir a senha click no link abaixo:", null);
                    string linha2 = Linha(false, "Link BioEasy", $"https://bioeasypro.club/redefinir?key={usuario.Id}&token={usuario.Senha}");
                    string linha3 = Linha(false, "Caso não tenha solicitado, favor desconsiderar o e-mail.", null);

                    html = html.Replace("[LINHA]", linha1 + linha2 + linha3);
                }

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = html;
                mailMessage.To.Add(email);

                var smtp = ConfiguracaoSMTP();

                smtp.Send(mailMessage);
                smtp.Dispose();
            }
            catch (Exception)
            {

                return;
            }
        }

        private SmtpClient ConfiguracaoSMTP()
        {
            var dadosConta = new Sistema().BuscarEmailSistema();
            return new SmtpClient
            {
                Host = "relay-hosting.secureserver.net",//"smtp.gmail.com",
                Port = 25, //587,
                EnableSsl = false,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(dadosConta.Email, dadosConta.Senha)
            };
        }
    }
}
