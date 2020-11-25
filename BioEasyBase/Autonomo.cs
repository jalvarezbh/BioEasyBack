using BioEasyBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BioEasyBase
{
    public class Autonomo : Base
    {
        public List<AutonomoModel> Listar()
        {
            try
            {
                List<AutonomoModel> autonomos = new List<AutonomoModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM AUTONOMO";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AutonomoModel autonomo = new AutonomoModel();
                                autonomo.Id = Guid.Parse(reader["ID"].ToString());
                                autonomo.Telefone = reader["TELEFONE"].ToString();
                                autonomo.Email = reader["EMAIL"].ToString();
                                autonomo.Instagram = reader["INSTAGRAM"].ToString();
                                autonomo.Logo = reader["LOGO"].ToString();
                                autonomo.Balanca = reader["BALANCA"].ToString();
                                autonomo.IdEndereco = Guid.Parse(reader["ID_ENDERECO"].ToString());
                                autonomo.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                                autonomos.Add(autonomo);
                            }
                            return autonomos;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AutonomoModel Buscar(string id)
        {
            try
            {
                AutonomoModel autonomo = new AutonomoModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM AUTONOMO
                                        WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe autonomo com esse identificador.");

                            while (reader.Read())
                            {
                                autonomo.Id = Guid.Parse(reader["ID"].ToString());                               
                                autonomo.Telefone = reader["TELEFONE"].ToString();
                                autonomo.Email = reader["EMAIL"].ToString();
                                autonomo.Instagram = reader["INSTAGRAM"].ToString();
                                autonomo.Logo = reader["LOGO"].ToString();
                                autonomo.Balanca = reader["BALANCA"].ToString();
                                autonomo.IdEndereco = Guid.Parse(reader["ID_ENDERECO"].ToString());
                                autonomo.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                            }

                            return autonomo;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AutonomoModel Gravar(AutonomoModel autonomo)
        {
            try
            {
                if (string.IsNullOrEmpty(autonomo.Id.ToString()) || autonomo.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(autonomo);
                    return registro;
                }
                else
                {
                    Alterar(autonomo);
                }

                return autonomo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AutonomoModel Incluir(AutonomoModel autonomo)
        {
            try
            {
                String telefone = Regex.Replace(autonomo.Telefone, @"[^\w\d]", "");
                autonomo.Telefone = telefone;

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO AUTONOMO (           
                                       TELEFONE,    
                                       EMAIL,
                                       INSTAGRAM,
                                       LOGO,   
                                       BALANCA,
                                       ID_ENDERECO,
                                       ATIVO)  
                                       OUTPUT INSERTED.ID
                                       VALUES (                                       
                                       '{autonomo.Telefone}',
                                       '{autonomo.Email}',
                                       '{autonomo.Instagram}',
                                       '{autonomo.Logo}',
                                       '{autonomo.Balanca}',
                                       '{autonomo.IdEndereco}',
                                        1)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        autonomo.Id = (Guid)command.ExecuteScalar();
                    }
                }

                return autonomo;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(AutonomoModel autonomo)
        {
            try
            {
                String telefone = Regex.Replace(autonomo.Telefone, @"[^\w\d]", "");
                autonomo.Telefone = telefone;

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE AUTONOMO SET                                               
                                       TELEFONE =  '{autonomo.Telefone}',   
                                       EMAIL =  '{autonomo.Email}',   
                                       INSTAGRAM =  '{autonomo.Instagram}',   
                                       LOGO = '{autonomo.Logo}',   
                                       BALANCA = '{autonomo.Balanca}',
                                       ATIVO = '{autonomo.Ativo}'
                                       WHERE ID = '{autonomo.Id}'";

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

    }
}
