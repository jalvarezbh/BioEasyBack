using BioEasyBase.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BioEasyBase
{
    public class Base
    {
        public SqlConnectionStringBuilder sqlConnection { get; set; }

        public Base()
        {
            if (sqlConnection == null)
            {
                //sqlConnection = new SqlConnectionStringBuilder();
                //sqlConnection.DataSource = "(LocalDb)\\MSSQLLocalDB";
                ////sqlConnection.UserID = "avantlifeadm";
                ////sqlConnection.Password = "JMAtalita13#";
                //sqlConnection.InitialCatalog = "BioEasyWeb";
                //sqlConnection.PersistSecurityInfo = false;
                //sqlConnection.TrustServerCertificate = true;

                sqlConnection = new SqlConnectionStringBuilder();
                sqlConnection.DataSource = "198.71.226.6";
                sqlConnection.UserID = "bioeasyweb";
                sqlConnection.Password = "#4wL5x8i";
                sqlConnection.InitialCatalog = "bioeasypro";
            }
        }

        public bool ValidarTempoAcesso(string id)
        {
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $"SELECT ID, ULTIMO_LOGIN FROM USUARIO WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("E-mail ou Senha Inválido");

                            while (reader.Read())
                            {
                                DateTime ultimo_login = !String.IsNullOrEmpty(reader["ULTIMO_LOGIN"].ToString()) ? Convert.ToDateTime(reader["ULTIMO_LOGIN"].ToString()) : DateTime.Now;
                                ultimo_login = ultimo_login.AddHours(10);
                                if (ultimo_login < DateTime.Now)
                                    return false;
                                else
                                    return true;
                            }

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static string Linha(bool strong, string texto, string link)
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(@"<p style='font-family: arial,  helvetica, sans-serif; font-size: 12px; color: #606060;'>");

            if (strong)
            {
                linha.Append(@"<strong>");
                linha.Append(texto);
                linha.Append(@"</strong>");
            }
            else if (!string.IsNullOrEmpty(link))
            {
                linha.Append($"<a href='{link}'>");
                linha.Append(@"<strong>");
                linha.Append(texto);
                linha.Append(@"</strong>");
                linha.Append(@"</a>");
            }
            else
                linha.Append(texto);

            linha.Append(@"</p>");

            return linha.ToString();
        }

        public static string LinhaLinkPDF(string texto, string link)
        {
            StringBuilder linha = new StringBuilder();
            linha.Append(@"<p style='font-family: arial,  helvetica, sans-serif; font-size: 16px; color: #606060;'>");
            linha.Append($"<a href='{link}'>");
            linha.Append(@"<strong>");
            linha.Append(texto);
            linha.Append(@"</strong>");
            linha.Append(@"</a>");
            linha.Append(@"</p>");

            return linha.ToString();
        }
    }
}
