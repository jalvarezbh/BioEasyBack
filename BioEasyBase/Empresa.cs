using BioEasyBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BioEasyBase
{
    public class Empresa : Base
    { 
        public List<EmpresaModel> Listar()
        {
            try
            {
                List<EmpresaModel> empresas = new List<EmpresaModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM EMPRESA";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmpresaModel empresa = new EmpresaModel();
                                empresa.Id = Guid.Parse(reader["ID"].ToString());
                                empresa.Nome = reader["NOME"].ToString();
                                empresa.CNPJ = reader["CNPJ"].ToString();
                                empresa.Telefone = reader["TELEFONE"].ToString();
                                empresa.Email = reader["EMAIL"].ToString();
                                empresa.Instagram = reader["INSTAGRAM"].ToString();
                                empresa.Logo = reader["LOGO"].ToString();
                                empresa.Balanca = reader["BALANCA"].ToString();
                                empresa.IdEndereco = Guid.Parse(reader["ID_ENDERECO"].ToString());
                                empresa.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                                empresas.Add(empresa);
                            }
                            return empresas;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<EmpresaModel> ListarAtivos()
        {
            try
            {
                List<EmpresaModel> empresas = new List<EmpresaModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM EMPRESA
                                        WHERE ATIVO = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmpresaModel empresa = new EmpresaModel();
                                empresa.Id = Guid.Parse(reader["ID"].ToString());
                                empresa.Nome = reader["NOME"].ToString();
                                empresa.CNPJ = reader["CNPJ"].ToString();
                                empresa.Telefone = reader["TELEFONE"].ToString();
                                empresa.Email = reader["EMAIL"].ToString();
                                empresa.Instagram = reader["INSTAGRAM"].ToString();
                                empresa.Logo = reader["LOGO"].ToString();
                                empresa.Balanca = reader["BALANCA"].ToString();
                                empresa.IdEndereco = Guid.Parse(reader["ID_ENDERECO"].ToString());
                                empresa.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                                empresas.Add(empresa);
                            }
                            return empresas;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EmpresaModel Buscar(string id)
        {
            try
            {
                EmpresaModel empresa = new EmpresaModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM EMPRESA
                                        WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe empresa com esse identificador.");

                            while (reader.Read())
                            {
                                empresa.Id = Guid.Parse(reader["ID"].ToString());
                                empresa.Nome = reader["NOME"].ToString();
                                empresa.CNPJ = reader["CNPJ"].ToString();
                                empresa.Telefone = reader["TELEFONE"].ToString();
                                empresa.Email = reader["EMAIL"].ToString();
                                empresa.Instagram = reader["INSTAGRAM"].ToString();
                                empresa.Logo = reader["LOGO"].ToString();
                                empresa.Balanca = reader["BALANCA"].ToString();
                                empresa.IdEndereco = Guid.Parse(reader["ID_ENDERECO"].ToString());
                                empresa.Ativo = Convert.ToBoolean(reader["ATIVO"]);
                            }

                            return empresa;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EmpresaModel Gravar(EmpresaModel empresa)
        {
            try
            {
               if(string.IsNullOrEmpty(empresa.Id.ToString()) || empresa.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(empresa);
                    return registro;
                }
               else
                {
                    Alterar(empresa);
                }

                return empresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EmpresaModel Incluir(EmpresaModel empresa)
        {
            try
            {  
                String telefone = Regex.Replace(empresa.Telefone, @"[^\w\d]", "");
                empresa.Telefone = telefone;
              
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO EMPRESA ( 
                                       NOME,              
                                       CNPJ,           
                                       TELEFONE,    
                                       EMAIL,
                                       INSTAGRAM,
                                       LOGO,   
                                       BALANCA,
                                       ID_ENDERECO,
                                       ATIVO)  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{empresa.Nome}',
                                       '{empresa.CNPJ}',
                                       '{empresa.Telefone}',
                                       '{empresa.Email}',
                                       '{empresa.Instagram}',
                                       '{empresa.Logo}',
                                       '{empresa.Balanca}',
                                       '{empresa.IdEndereco}',
                                       '{empresa.Ativo}' )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        empresa.Id = (Guid)command.ExecuteScalar();
                    }
                }

                return empresa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(EmpresaModel empresa)
        {
            try
            {    
                String telefone = Regex.Replace(empresa.Telefone, @"[^\w\d]", "");
                empresa.Telefone = telefone;                

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE EMPRESA SET 
                                       NOME = '{empresa.Nome}',              
                                       CNPJ = '{empresa.CNPJ}',           
                                       TELEFONE =  '{empresa.Telefone}',   
                                       EMAIL =  '{empresa.Email}',   
                                       INSTAGRAM =  '{empresa.Instagram}',   
                                       LOGO = '{empresa.Logo}',   
                                       BALANCA = '{empresa.Balanca}',
                                       ATIVO = '{empresa.Ativo}'
                                       WHERE ID = '{empresa.Id}'";

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
                                        FROM EMPRESA
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
    }
}
