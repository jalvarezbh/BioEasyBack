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
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BioEasyBase
{
    public class Sistema : Base
    {
        public SistemaModel Buscar()
        {
            try
            {
                SistemaModel sistema = new SistemaModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM SISTEMA
                                        WHERE ATIVO = 1
                                        ORDER BY DATA_ATUALIZACAO DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe configuração de sistema ativa.");

                            while (reader.Read())
                            {
                                sistema.Id = Guid.Parse(reader["ID"].ToString());
                                sistema.Responsavel = reader["RESPONSAVEL"].ToString();
                                sistema.Email = reader["EMAIL"].ToString();
                                sistema.DataAtualizacao = Convert.ToDateTime(reader["DATA_ATUALIZACAO"].ToString());
                                sistema.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                sistema.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                            }

                            return sistema;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SistemaModel BuscarEmailSistema()
        {
            try
            {
                SistemaModel sistema = new SistemaModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM SISTEMA
                                        WHERE ATIVO = 1
                                        ORDER BY DATA_ATUALIZACAO DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe configuração de sistema ativa.");

                            while (reader.Read())
                            {
                                sistema.Id = Guid.Parse(reader["ID"].ToString());
                                sistema.Email = reader["EMAIL"].ToString();
                                sistema.Senha = reader["SENHA"].ToString();
                                sistema.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                            }

                            sistema.Senha = DecryptString(sistema.Senha);
                            return sistema;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SistemaModel Incluir(SistemaModel sistema)
        {
            try
            {
                if (sistema.Senha != sistema.SenhaRepetir)
                    throw new Exception("Campo Senha diferente do Campo Confirmação.");
                               
                string hashed = EncryptString(sistema.Senha);
                string dataAtualizacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO SISTEMA (         
                                       EMAIL,
                                       SENHA,
                                       RESPONSAVEL,
                                       DATA_ATUALIZACAO,
                                       ID_USUARIO,
                                       ATIVO)  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{sistema.Email}',
                                       '{hashed}',
                                       '{sistema.Responsavel}',
                                       '{dataAtualizacao}',
                                       '{sistema.IdUsuario}',
                                       '1' )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        sistema.Id = (Guid)command.ExecuteScalar();
                        Desativar(sistema);
                    }

                    
                }

                return sistema;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Desativar(SistemaModel sistema)
        {
            try
            {                
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE SISTEMA 
                                       SET ATIVO = 0
                                       WHERE ATIVO = 1 
                                         AND ID <> '{sistema.Id}'";

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
    }
}
