using BioEasyBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BioEasyBase
{
    public class Laboratorial : Base
    {
        public List<LaboratorialModel> Listar(string idPaciente)
        {
            try
            {
                List<LaboratorialModel> laboratoriais = new List<LaboratorialModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM LABORATORIAL
                                        WHERE ID_PACIENTE = '{idPaciente}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LaboratorialModel laboratorial = new LaboratorialModel();                               
                                laboratorial.Id = Guid.Parse(reader["ID"].ToString());
                                laboratorial.Nome = reader["NOME"].ToString();
                                laboratorial.Colesterol = Convert.ToDecimal(reader["COLESTEROL"]);
                                laboratorial.LDL = Convert.ToDecimal(reader["LDL"]);
                                laboratorial.HDL = Convert.ToDecimal(reader["HDL"]);
                                laboratorial.Trigliceres = Convert.ToDecimal(reader["TRIGLICERES"]);
                                laboratorial.Acucar = Convert.ToDecimal(reader["ACUCAR"]);
                                laboratorial.UltimaRefeicao = Convert.ToInt16(reader["ULTIMA_REFEICAO"]);

                                if (!string.IsNullOrEmpty(reader["DATA_EXAME"].ToString()))
                                {
                                    laboratorial.DataExame = Convert.ToDateTime(reader["DATA_EXAME"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    laboratorial.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    laboratorial.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    laboratorial.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    laboratorial.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }

                                laboratoriais.Add(laboratorial);
                            }
                            return laboratoriais;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LaboratorialModel Buscar(string id)
        {
            try
            {
                LaboratorialModel laboratorial = new LaboratorialModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM LABORATORIAL
                                        WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe dados laboratoriais com esse identificador.");

                            while (reader.Read())
                            {
                                laboratorial.Id = Guid.Parse(reader["ID"].ToString());
                                laboratorial.Nome = reader["NOME"].ToString();
                                laboratorial.Colesterol = Convert.ToDecimal(reader["COLESTEROL"]);
                                laboratorial.LDL = Convert.ToDecimal(reader["LDL"]);
                                laboratorial.HDL = Convert.ToDecimal(reader["HDL"]);
                                laboratorial.Trigliceres = Convert.ToDecimal(reader["TRIGLICERES"]);
                                laboratorial.Acucar = Convert.ToDecimal(reader["ACUCAR"]);
                                laboratorial.UltimaRefeicao = Convert.ToInt16(reader["ULTIMA_REFEICAO"]);

                                if (!string.IsNullOrEmpty(reader["DATA_EXAME"].ToString()))
                                {
                                    laboratorial.DataExame = Convert.ToDateTime(reader["DATA_EXAME"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    laboratorial.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    laboratorial.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    laboratorial.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    laboratorial.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }
                            }

                            return laboratorial;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LaboratorialModel Gravar(LaboratorialModel laboratorial)
        {
            try
            {
                if (string.IsNullOrEmpty(laboratorial.Id.ToString()) || laboratorial.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(laboratorial);
                    return registro;
                }
                else
                {
                    Alterar(laboratorial);
                }

                return laboratorial;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LaboratorialModel Incluir(LaboratorialModel laboratorial)
        {
            try
            {
                string dataExame = laboratorial.DataExame.ToString("yyyy-MM-dd HH:mm:ss");
                string dataAvaliacao = laboratorial.DataAvaliacao.ToString("yyyy-MM-dd HH:mm:ss");
               
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO LABORATORIAL ( 
                                       NOME,              
                                       COLESTEROL,           
                                       LDL,    
                                       HDL,
                                       TRIGLICERES,
                                       ACUCAR,   
                                       DATA_EXAME,
                                       DATA_AVALIACAO,
                                       ULTIMA_REFEICAO,
                                       ID_PACIENTE,
                                       ID_USUARIO,
                                       ID_EMPRESA)  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{laboratorial.Nome}',                                       
                                        {laboratorial.Colesterol.ToString().Replace(',', '.')},
                                        {laboratorial.LDL.ToString().Replace(',', '.')},
                                        {laboratorial.HDL.ToString().Replace(',', '.')},
                                        {laboratorial.Trigliceres.ToString().Replace(',', '.')},
                                        {laboratorial.Acucar.ToString().Replace(',', '.')},
                                       '{dataExame}',
                                       '{dataAvaliacao}',     
                                        {laboratorial.UltimaRefeicao},
                                       '{laboratorial.IdPaciente}',
                                       '{laboratorial.IdUsuario}',
                                       '{laboratorial.IdEmpresa}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        laboratorial.Id = (Guid)command.ExecuteScalar();
                    }
                }

                return laboratorial;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(LaboratorialModel laboratorial)
        {
            try
            {
                string dataExame = laboratorial.DataExame.ToString("yyyy-MM-dd HH:mm:ss");
                string dataAvaliacao = laboratorial.DataAvaliacao.ToString("yyyy-MM-dd HH:mm:ss");


                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE LABORATORIAL SET 
                                       NOME = '{laboratorial.Nome}',              
                                       COLESTEROL = {laboratorial.Colesterol.ToString().Replace(',', '.')},           
                                       LDL =  {laboratorial.LDL.ToString().Replace(',', '.')},   
                                       HDL =  {laboratorial.HDL.ToString().Replace(',', '.')},   
                                       TRIGLICERES =  {laboratorial.Trigliceres.ToString().Replace(',', '.')},   
                                       ACUCAR =  {laboratorial.Acucar.ToString().Replace(',', '.')},   
                                       DATA_EXAME =  '{dataExame}', 
                                       DATA_AVALIACAO =  '{dataAvaliacao}',
                                       ULTIMA_REFEICAO =  {laboratorial.UltimaRefeicao}";
                    

                    query += $"  WHERE ID = '{laboratorial.Id}'";

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
